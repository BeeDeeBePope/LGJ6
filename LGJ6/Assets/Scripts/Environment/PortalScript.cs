using UnityEngine;

namespace Environment
{
    public class PortalScript : MonoBehaviour
    {
        public PortalScript Destination;
        
        private bool arrived;

        private void Awake()
        {
            if (Destination != null)
            {
                Destination.Destination = this;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (arrived)
            {
                return;
            }
            if (other.CompareTag("Player"))
            {
                Despawn(other.gameObject);
                Destination.Spawn(other.gameObject);
            }
        }

        private void Despawn(GameObject player)
        {
            player.SetActive(false);
        }

        public void Spawn(GameObject player)
        {
            arrived = true;
            player.transform.rotation = transform.rotation;
            Vector3 pos = player.transform.position;
            pos.x = transform.position.x;
            pos.z = transform.position.z;
            player.transform.position = pos;
            player.SetActive(true);
        }
    }
}
