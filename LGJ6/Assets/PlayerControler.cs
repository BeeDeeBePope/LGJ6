using Player;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public PlayerMovement Movement;
    public Collider PlayerCollider;
    public GameObject PlayerVisuals;

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
        PlayerVisuals.SetActive(true);
        Movement.enabled = true;
        PlayerCollider.enabled = true;
    }
}
