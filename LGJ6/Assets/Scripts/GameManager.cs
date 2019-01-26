using System.Collections;
using Player;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UiManager UiManager;

    public PlayerControler Player;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
        UiManager = FindObjectOfType<UiManager>();
        Player = FindObjectOfType<PlayerControler>();
    }

    public void StartCountingPoints()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(CountPoints());
        }
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