using RichUnity.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject EndGameMenu;
        public GameObject HUD;
        public GameObject MusicToggleButtom;
        public TextMeshProUGUI scoretext;
        public TextMeshProUGUI highscoretext;
        public FloatVariable highscore;
        public TextMeshProUGUI currrentscoretext;
        public Image SoundsImage;
        public Sprite On;
        public Sprite Off;
        public AudioMixerVolumeController AudioMixer;

        public void Awake()
        {
            if (AudioMixer.Muted)
            {
                SoundsImage.sprite = Off;
                GameManager.Instance.soundSource.volume = 0;
                GameManager.Instance.musicSource.volume = 0;
            }
            else
            {
                SoundsImage.sprite = On;
                GameManager.Instance.soundSource.volume = 1;
                GameManager.Instance.musicSource.volume = 1;
            }
        }


        public void ShowMainMenu()
        {
            MainMenu.SetActive(true);
            EndGameMenu.SetActive(false);
            HUD.SetActive(false);
            GameManager.Instance.RestartBoard();
        }

        public void ShowEndGame()
        {
            MainMenu.SetActive(false);
            EndGameMenu.SetActive(true);
            HUD.SetActive(false);
        }

        public void ShowHUD()
        {
            MainMenu.SetActive(false);
            EndGameMenu.SetActive(false);
            HUD.SetActive(true);
        }

        public void StartGame()
        {
            SetCurrentPoints(0);
            ShowHUD();
            GameManager.Instance.StartGame();
        }

        public void RestartGame()
        {
            SetCurrentPoints(0);
            ShowHUD();
            GameManager.Instance.Restart();
        }

        public void SetScore(int score)
        {
            scoretext.text = score.ToString();
            if (score > highscore.value)
            {
                highscore.value = score;
            }
            highscoretext.text = highscore.value.ToString();
        }

        public void SetCurrentPoints(int score)
        {
            currrentscoretext.text = score.ToString();
        }

        public void ToggleMusic()
        {
            AudioMixer.Muted = !AudioMixer.Muted;

            if (AudioMixer.Muted)
            {
                SoundsImage.sprite = Off;
                GameManager.Instance.soundSource.volume = 0;
                GameManager.Instance.musicSource.volume = 0;
            }
            else
            {
                SoundsImage.sprite = On;
                GameManager.Instance.soundSource.volume = 1;
                GameManager.Instance.musicSource.volume = 1;
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}