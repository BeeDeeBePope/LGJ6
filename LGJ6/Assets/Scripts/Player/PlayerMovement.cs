using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IMovementScript
    {
        public Vector3 CurrentDirection;
        public float Speed;
        public float increaseSpeed;
        [HideInInspector] public PlayerControler Player;
        private float baseSpeed;

        private void Awake()
        {
            if (CurrentDirection == Vector3.zero)
            {
                CurrentDirection = transform.right;
            }
            baseSpeed = Speed;
        }

        private void Update()
        {
            transform.position += CurrentDirection * Speed * Time.deltaTime;
        }

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
        }

        public void ResetSpeed()
        {
            Speed = baseSpeed;
        }
    }
}