using UnityEngine;

namespace Environment.MapElements
{
    public class PlayerKiller : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
            }
        }
    }
}
