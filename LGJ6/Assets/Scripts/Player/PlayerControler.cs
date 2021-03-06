﻿using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerControler : MonoBehaviour
    {
        public PlayerMovement Movement;
        public Collider PlayerCollider;
        public GameObject PlayerVisuals;

        //public int CoinCount;

        private void Awake()
        {
            Movement = GetComponent<PlayerMovement>();
            Movement.Player = this;
            PlayerCollider = GetComponent<Collider>();
        }

        public void HidePlayer()
        {
            PlayerVisuals.SetActive(false);
            Movement.enabled = false;
            PlayerCollider.enabled = false;
        }

        public void ShowPlayer(float wait = 0)
        {
            PlayerVisuals.SetActive(true);
            StartCoroutine(WaitForStart(wait));
        }

        IEnumerator WaitForStart(float wait)
        {
            yield return new WaitForSeconds(wait);
            Movement.enabled = true;
            PlayerCollider.enabled = true;
        }

        public void Die()
        {
            HidePlayer();
            GameManager.Instance.EndGame();
        }

        public void AddCoin()
        {
            GameManager.Instance.AddPointsForGold();
        }

        public void TurnRight()
        {
            var scale = PlayerVisuals.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1;
            PlayerVisuals.transform.localScale = scale;
        }

        public void TurnLeft()
        {
            var scale = PlayerVisuals.transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            PlayerVisuals.transform.localScale = scale;
        }
    }
}
