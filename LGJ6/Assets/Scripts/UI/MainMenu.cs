using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            GameManager.Instance.StartGame();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
