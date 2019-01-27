using System.Collections;
using Player;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject board;
    public float pointsForRoom=100;
    public float pointsForGold = 25;
    public float pointsForNewBoard = 25;
    public float allPoints;

    private float remainingPoints;
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
        allPoints = 0;
        StartCountingPoints();
        Player.Movement.ResetSpeed();
        Player.ShowPlayer();
    }

    public void PauseGame()
    {
    }

    public void EndGame()
    {
        StopCountingPoints();
        Player.Movement.enabled = false;
        UiManager.SetScore(Mathf.CeilToInt(allPoints));
        UiManager.ShowEndGame();
    }

    public void StartCountingPoints()
    {
        ResetAvilablePoints();
        if (coroutine == null)
        {
            coroutine = StartCoroutine(CountPoints());
        }
    }

    public void RestartBoard()
    {
        Destroy(spawnedBoard);
        spawnedBoard = Instantiate(board);
    }

    public void Restart()
    {
        allPoints = 0;
        Destroy(spawnedBoard);
        StartCountingPoints();
        Player.Movement.ResetSpeed();
        Player.ShowPlayer();
        spawnedBoard = Instantiate(board);
    }

    public void ResetAvilablePoints()
    {
        remainingPoints = pointsForRoom;
    }

    private IEnumerator CountPoints()
    {
        while(remainingPoints > 0)
        {
            yield return new WaitForSeconds(.1f);
            remainingPoints--;
            allPoints++;
            UiManager.SetCurrentPoints(Mathf.CeilToInt(allPoints));
        }        
    }

    public void AddPointsForGold()
    {
        allPoints += pointsForGold;
        UiManager.SetCurrentPoints(Mathf.CeilToInt(allPoints));
    }

    public void AddPointsForNewBoard()
    {
        allPoints += pointsForNewBoard;
        UiManager.SetCurrentPoints(Mathf.CeilToInt(allPoints));
    }

    public void StopCountingPoints()
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }
}