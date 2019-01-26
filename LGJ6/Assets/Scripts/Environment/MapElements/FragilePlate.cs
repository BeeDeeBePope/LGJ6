using UnityEngine;

namespace Environment.MapElements
{
    public class FragilePlate : MonoBehaviour
    {
        public GameObject PreVisuals;
        public GameObject PostVisuals;

        private PlayerKiller pk;

        private void Awake()
        {
            PreVisuals.SetActive(true);
            PostVisuals.SetActive(false);
            pk = GetComponent<PlayerKiller>();
            pk.DisableOverride = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PreVisuals.SetActive(false);
                PostVisuals.SetActive(true);
                pk.DisableOverride = false;
            }
        }
    }
}
