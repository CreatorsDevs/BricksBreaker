using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject selectionScreenUI;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private bool isGameOver = false;
    public bool IsGameOver { get => isGameOver; private set => isGameOver = value; }

    public void Gameover()
    {
        IsGameOver = true;
        gameOverUI.SetActive(true);
        ScoreManager.Score = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnHome()
    {
        SceneManager.LoadScene(0); //LobbyScene
    }
    public void Play()
    {
        selectionScreenUI.SetActive(true);
    }
    public void back()
    {
        selectionScreenUI.SetActive(false);
    }
    public void PlayButtonSound()
    {
        AudioManager.instance.Play(Strings.buttonAudio);
    }
}
