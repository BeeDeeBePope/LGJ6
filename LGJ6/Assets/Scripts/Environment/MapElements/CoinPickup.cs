using DG.Tweening;
using Player;
using UnityEngine;

namespace Environment.MapElements
{
    public class CoinPickup : MonoBehaviour
    {
        public float RotationSpeed = 1;
        private Tweener tweener;

        // Start is called before the first frame update
        void Start()
        {
            tweener = transform.DORotateQuaternion(Quaternion.AngleAxis(180, transform.up), RotationSpeed).SetLoops(-1);
            tweener.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tweener.Kill();
                var pl = other.GetComponent<PlayerControler>();
                pl.AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
