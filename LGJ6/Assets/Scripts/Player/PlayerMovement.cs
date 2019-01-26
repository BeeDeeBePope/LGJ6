using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IMovementScript
    {
        public Vector3 CurrentDirection;
        public float Speed;
        public float increaseSpeed;
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
            CurrentDirection.x = direction.x;
            CurrentDirection.z = direction.y;
        }

        public void SpeedUP()
        {
            Speed += increaseSpeed;
        }

        public void ResetSpeed()
        {
            Speed = baseSpeed;
        }
    }
}