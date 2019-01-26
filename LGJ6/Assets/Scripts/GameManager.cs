using System.Collections;
using Player;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject board;
    private GameObject spawnedBoard;


    [HideInInspector] public UiManager UiManager;
    [HideInInspector] public PlayerControler Player;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
        UiManager = FindObjectOfType<UiManager>();
        Player = FindObjectOfType<PlayerControler>();
        spawnedBoard = Instantiate(board);
    }

    private void Start()
    {
        Player.HidePlayer();
    }

    public void StartGame()
    {
        Player.ShowPlayer();
        Player.transform.position = new Vector3(0,12,0);
    }

    public void PauseGame()
    {
    }

    public void EndGame()
    {
        Player.Movement.enabled = false;
        UiManager.ShowEndGame();
    }

    public void StartCountingPoints()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(CountPoints());
        }
    }

    public void Restart()
    {
        Destroy(spawnedBoard);
        spawnedBoard = Instantiate(board);
    }

    private IEnumerator CountPoints()
    {
        yield return null;
    }

    public void StopCountingPoints()
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }
}