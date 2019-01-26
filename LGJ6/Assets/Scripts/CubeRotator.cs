using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CubeRotator : MonoBehaviour
{
    public static CubeRotator Instance;
    public UnityEvent unityEvent;

    GameObject last;

    public List<GameObject> planes;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        planes = new List<GameObject>();
        Transform[] trans = GetComponentsInChildren<Transform>();
        foreach(Transform transform in trans)
        {
                planes.Add(transform.gameObject);
        }

        planes.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        unityEvent.Invoke();
    }

    public void Rotate(Vector2 direction)
    {
        // GENERUJ MAPE 
        foreach(GameObject game in planes)
        {
            if(game.transform.eulerAngles == Vector3.zero)
            {
                last = game;
            }
        }

        transform.DORotate(transform.eulerAngles + new Vector3(-direction.y, 0, direction.x) * 90, 1f, RotateMode.Fast).OnComplete(ResetRotation);
    }

    private void ResetRotation()
    {
        GameObject neww =null;

        foreach (GameObject game in planes)
        {
            if (game.transform.eulerAngles == Vector3.zero)
            {
                neww = game;
            }
        }

        // DO wyrzucenia
        Quaternion qq = last.transform.rotation;
        Vector3 vec3 = last.transform.position;
        last.transform.position = neww.transform.position;
        last.transform.rotation = neww.transform.rotation;

        neww.transform.position = vec3;
        neww.transform.rotation = qq;
        transform.rotation = Quaternion.identity;
        unityEvent.Invoke();
    }
}
