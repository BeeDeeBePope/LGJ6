using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject EndGameMenu;

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
            GameManager.Instance.StartGame();
            MainMenu.SetActive(false);
            EndGameMenu.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}