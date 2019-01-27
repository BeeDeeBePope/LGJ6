using System.Collections;
using DG.Tweening;
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

    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioClip startSound;
    public AudioClip deathSound;
    public AudioClip menuSound;
    public AudioClip[] musicSound;

    private float remainingPoints;
    private GameObject spawnedBoard;


    [HideInInspector] public UiManager UiManager;
    [HideInInspector] public PlayerControler Player;

    private Coroutine coroutine;
    private Coroutine musiccoroutine;
    [SerializeField] public AnimationCurve CameraCurve;

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
        musicSource.clip = menuSound;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void StartGame()
    {
        allPoints = 0;
        StartCountingPoints();
        StartMusic(startSound.length);
        Player.Movement.ResetSpeed();

        musicSource.Stop();
        musicSource.loop = false;
        soundSource.clip = startSound;
        soundSource.Play();

        Player.ShowPlayer(startSound.length);
    }

    public void PauseGame()
    {
    }

    public void EndGame()
    {
        StopCountingPoints();
        StopMusic();
        Player.Movement.enabled = false;
        UiManager.SetScore(Mathf.CeilToInt(allPoints));
        UiManager.ShowEndGame();

        musicSource.Stop();
        soundSource.clip = deathSound;
        soundSource.Play();
    }

    public void RestartBoard()
    {
        Destroy(spawnedBoard);
        spawnedBoard = Instantiate(board);
        musicSource.clip = menuSound;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void Restart()
    {
        allPoints = 0;
        Destroy(spawnedBoard);
        StartCountingPoints();
        StartMusic(startSound.length);
        Player.Movement.ResetSpeed();
        Player.ShowPlayer(startSound.length);
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

    private IEnumerator PlayMusic(float time)
    {
        yield return new WaitForSeconds(time);
        while(true)
        {           
            int i = Random.Range(0,musicSound.Length);
            musicSource.clip = musicSound[i];
            musicSource.Play();
            yield return new WaitForSeconds(musicSound[i].length);
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

    public void StartCountingPoints()
    {
        ResetAvilablePoints();
        if (coroutine == null)
        {
            coroutine = StartCoroutine(CountPoints());
        }
    }

    public void StopCountingPoints()
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }

    public void StartMusic(float time)
    {
        if (musiccoroutine == null)
        {
            musiccoroutine = StartCoroutine(PlayMusic(time));
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        StopCoroutine(musiccoroutine);
        musiccoroutine = null;
    }
}