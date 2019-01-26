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

        private bool arrived;

        //private void Awake()
        //{
        //    if (Destination != null)
        //    {
        //        Destination.Destination = this;
        //    }
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (arrived)
            {
                return;
            }
            if (other.CompareTag("Player"))
            {
                PlayerControler pl = other.GetComponent<PlayerControler>();
                pl.HidePlayer();

                CubeRotator.Instance.Rotate(DirectionToVector());
                //other.transform.DOMove(Destination.transform.localPosition,1f);
                CubeRotator.Instance.unityEvent.AddListener(() =>
                {
                    Destination.Spawn(other.gameObject);
                    pl.ShowPlayer();
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
        
        public void Spawn(GameObject player)
        {
            arrived = true;
            player.transform.rotation = transform.rotation;
            Vector3 pos = player.transform.position;
            pos.x = transform.position.x;
            pos.z = transform.position.z;
            player.transform.position = pos;

            Destroy(gameObject);
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
