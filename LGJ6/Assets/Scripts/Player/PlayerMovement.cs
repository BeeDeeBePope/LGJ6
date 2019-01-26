using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IMovementScript
    {
        public Vector3 CurrentDirection;
        public float Speed;

        private void Awake()
        {
            if (CurrentDirection == Vector3.zero)
            {
                CurrentDirection = transform.right;
            }
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
    }
}