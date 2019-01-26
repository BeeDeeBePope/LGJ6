using DG.Tweening;
using Player;
using UnityEngine;

namespace Environment.MapElements
{
    public class CoinPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var pl = other.GetComponent<PlayerControler>();
                pl.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
