﻿using TMPro;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject EndGameMenu;
        public TextMeshProUGUI scoretext;

        public void ShowMainMenu()
        {
            MainMenu.SetActive(true);
            EndGameMenu.SetActive(false);
        }

        public void ShowEndGame()
        {
            MainMenu.SetActive(false);
            EndGameMenu.SetActive(true);
        }

        public void StartGame()
        {
            MainMenu.SetActive(false);
            GameManager.Instance.StartGame();
        }

        public void RestartGame()
        {
            EndGameMenu.SetActive(false);
            GameManager.Instance.Restart();
        }

        public void SetScore(int score)
        {
            scoretext.text = score.ToString();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}