using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IMovementScript
    {
        public Vector3 CurrentDirection;
        public float Speed;
        public float increaseSpeed;
        [HideInInspector] public PlayerControler Player;
        private float baseSpeed;
        private float timeforonegrid;
        Coroutine onegridmove;

        private void Awake()
        {
            if (CurrentDirection == Vector3.zero)
            {
                CurrentDirection = transform.right;
            }
            baseSpeed = Speed;
            timeforonegrid = 1 / (Speed);
        }

        private void OnEnable()
        {
            onegridmove = StartCoroutine(move());
        }

        private void OnDisable()
        {
            StopCoroutine(onegridmove);
        }

        private IEnumerator move()
        {
            while(true)
            {
                transform.DOMove(transform.position + CurrentDirection, timeforonegrid);
                yield return new WaitForSeconds(timeforonegrid);
            }
        }

        //private void Update()
        //{
        //    transform.position += CurrentDirection * Speed * Time.deltaTime;
        //}

        public void SetDirection(Vector2 direction)
        {
            if (Player != null)
            {
                if (direction.x >= 0)
                {
                    Player.TurnRight();
                }
                else
                {
                    Player.TurnLeft();
                }
            }

            CurrentDirection.x = direction.x;
            CurrentDirection.z = direction.y;
        }

        public void SpeedUp()
        {
            Speed += increaseSpeed;
            timeforonegrid = 1 / (Speed);
        }

        public void ResetSpeed()
        {
            Speed = baseSpeed;
            timeforonegrid = 1 / (Speed);
        }
    }
}