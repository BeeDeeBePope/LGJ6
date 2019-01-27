using DG.Tweening;
using Player;
using UnityEngine;

namespace Environment.MapElements
{
    public class CoinPickup : MonoBehaviour
    {
        public GameObject PreVisuals;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PreVisuals.SetActive(false);
                var pl = other.GetComponent<PlayerControler>();
                pl.AddCoin();
                Destroy(this);
            }
        }
    }
}
