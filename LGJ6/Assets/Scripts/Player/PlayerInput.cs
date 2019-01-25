using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        PlayerMovement playerMovement;
        Vector2 startPosition;
        Vector2 endPosition;


        // Start is called before the first frame update
        void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touches.Length == 0) return;

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startPosition = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                endPosition = Input.touches[0].position;
                DetectDirection();
            }
        }

        void DetectDirection()
        {
            if (Mathf.Abs(startPosition.x - endPosition.x) > Mathf.Abs(startPosition.y - endPosition.y))
            {
                if (startPosition.x > endPosition.x)
                {
                    playerMovement.SetDirection(Vector2.left);
                }
                else
                {
                    playerMovement.SetDirection(Vector2.right);
                }
            }
            else
            {
                if (startPosition.y > endPosition.y)
                {
                    playerMovement.SetDirection(Vector2.down);
                }
                else
                {
                    playerMovement.SetDirection(Vector2.up);
                }
            }
        }
    }
}
