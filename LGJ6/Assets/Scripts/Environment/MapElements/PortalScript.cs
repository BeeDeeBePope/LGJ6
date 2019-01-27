using System;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Environment.MapElements
{
    public class PortalScript : MonoBehaviour
    {
        public PortalScript Destination;

        public Direction Direction;

        public bool arrived;

        public GameObject PreVisuals;
        public GameObject PostVisuals;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(transform.position);

            if (arrived)
            {
                return;
            }
            if (other.CompareTag("Player"))
            {
                arrived = true;
                PlayerControler pl = other.GetComponent<PlayerControler>();
                pl.HidePlayer();

                Destination.SetArrived();
                var dest = Destination.transform.localPosition;
                dest.x += DirectionToVector().x;
                dest.y = Destination.transform.position.y;
                dest.z += DirectionToVector().y;
                other.transform.DOLocalMove(dest, 1f).SetEase(GameManager.Instance.CameraCurve);
                CubeRotator.Instance.Rotate(DirectionToVector());
                CubeRotator.Instance.unityEvent.AddListener(() =>
                {
                    Destination.Spawn(other.gameObject);
                    pl.ShowPlayer(0.6f);
                });
            }
        }

        private Vector2 DirectionToVector()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return Vector2.up;
                case Direction.Down:
                    return Vector2.down;
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                    return Vector2.right;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void SetArrived()
        {
            arrived = true;
            Debug.Log("b");
        }

        public void Spawn(GameObject player)
        {
            player.transform.rotation = transform.rotation;
            Debug.Log(player.GetComponent<PlayerMovement>().CurrentDirection);
            Debug.Log(transform.position);
            player.transform.position = transform.position + player.GetComponent<PlayerMovement>().CurrentDirection;
            Debug.Log(player.transform.position);

            player.GetComponent<PlayerMovement>().SpeedUp();

            PreVisuals.SetActive(false);
            PostVisuals.SetActive(true);
            GetComponent<PlayerKiller>().DisableOverride = false;
            Destroy(this);
        }

        private void OnDestroy()
        {
            if(CubeRotator.Instance !=null)
            CubeRotator.Instance.unityEvent.RemoveAllListeners();
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
