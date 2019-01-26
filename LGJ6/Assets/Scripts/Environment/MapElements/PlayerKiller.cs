using Player;
using UnityEngine;

namespace Environment.MapElements
{
    public class PlayerKiller : MonoBehaviour
    {
        public bool DisableOverride;

        private void OnTriggerEnter(Collider other)
        {
            if (DisableOverride)
            {
                return;
            }

            if (other.CompareTag("Player"))
            {
                PlayerControler pl = other.GetComponent<PlayerControler>();
                pl.Die(); 
            }
        }
    }
}
