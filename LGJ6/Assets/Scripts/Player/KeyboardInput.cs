using System;
using UnityEngine;

namespace Player
{
    public class KeyboardInput : MonoBehaviour
    {
        private PlayerMovement movement;

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement.SetDirection(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement.SetDirection(Vector2.down);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                movement.SetDirection(Vector2.right);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                movement.SetDirection(Vector2.up);
            }
        }
    }
}
