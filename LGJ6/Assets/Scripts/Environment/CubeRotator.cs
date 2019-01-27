using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Environment
{
    public class CubeRotator : MonoBehaviour
    {
        public static CubeRotator Instance;
        public UnityEvent unityEvent;

        public GameObject last;
        public MapGenerator mapGenerator;

        public List<GameObject> planes;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            mapGenerator = GetComponent<MapGenerator>();

            planes = new List<GameObject>();
            Transform[] trans = GetComponentsInChildren<Transform>();
            foreach (Transform transform in trans)
            {
                if (transform.CompareTag("Plane")) planes.Add(transform.gameObject);
            }
        }


        public void Rotate(Vector2 direction)
        {
            planes = new List<GameObject>();
            Transform[] trans = GetComponentsInChildren<Transform>();
            foreach (Transform transform in trans)
            {
                if (transform.CompareTag("Plane")) planes.Add(transform.gameObject);
            }


            foreach (GameObject game in planes)
            {
                if (game.transform.eulerAngles == Vector3.zero)
                {
                    last = game;
                }
            }
            transform.DORotate(transform.eulerAngles + new Vector3(-direction.y, 0, direction.x) * 90, 1f, RotateMode.Fast).OnComplete(ResetRotation);
        }

        private void ResetRotation()
        {
            foreach (GameObject game in planes)
            {
                if (game.transform.eulerAngles == Vector3.zero)
                {
                    game.transform.position = last.transform.position;
                    game.transform.rotation = last.transform.rotation;

                    transform.rotation = Quaternion.identity;
                    Debug.Log("Reset");
                    mapGenerator.ChangeMainBorder(game);
                    unityEvent.Invoke();

                    GameManager.Instance.AddPointsForNewBoard();
                    GameManager.Instance.StopCountingPoints();
                    GameManager.Instance.StartCountingPoints();

                    break;
                }
            }
        }
    }
}
