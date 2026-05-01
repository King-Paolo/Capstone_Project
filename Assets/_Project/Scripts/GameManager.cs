using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _victoryMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private AudioClip _backgroundMusic;

    private bool _isPaused;
    private bool _gameEnded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(_backgroundMusic);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (_gameEnded) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0;
        //MenuManager.Instance.PauseMenu(_pauseMenu, true);
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        //MenuManager.Instance.PauseMenu(_pauseMenu, false);
    }

    public void Victory()
    {
        _gameEnded = true;
        //MenuManager.Instance.VictoryMenu(_victoryMenu);
        Time.timeScale = 0;
    }

    public void TriggerDelayVictory(float delay)
    {
        StartCoroutine(DelayVictory(delay));
    }
    public void GameOver()
    {
        _gameEnded = true;
        //MenuManager.Instance.GameOverMenu(_gameOverMenu);
        Time.timeScale = 0;
    }

    public void TriggerDelayGameOver(float delay)
    {
        StartCoroutine(DelayGameOver(delay));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        AudioManager.Instance.StopMusic();
    }

    public IEnumerator DelayGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameOver();
    }

    public IEnumerator DelayVictory(float delay)
    {
        yield return new WaitForSeconds(delay);
        Victory();
    }
}
