using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerControler : MonoBehaviour
    {
        public PlayerMovement Movement;
        public Collider PlayerCollider;
        public GameObject PlayerVisuals;

        public int CoinCount;

        private void Awake()
        {
            Movement = GetComponent<PlayerMovement>();
            PlayerCollider = GetComponent<Collider>();
        }

        public void HidePlayer()
        {
            PlayerVisuals.SetActive(false);
            Movement.enabled = false;
            PlayerCollider.enabled = false;
        }

        public void ShowPlayer()
        {
            StartCoroutine(WaitForShowPlayer());
        }

        IEnumerator WaitForShowPlayer()
        {
            yield return new WaitForSeconds(.2f);
            PlayerVisuals.SetActive(true);
            Movement.enabled = true;
            PlayerCollider.enabled = true;
        }

        public void Die()
        {
            HidePlayer();
        }

        public void AddCoin()
        {
            CoinCount++;
        }
    }
}
