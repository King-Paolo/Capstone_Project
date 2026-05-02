using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void VictoryMenu(GameObject victoryMenu)
    {
        if (victoryMenu != null)
            victoryMenu.SetActive(true);
    }

    public void GameOverMenu(GameObject gameOverMenu)
    {
        if (gameOverMenu != null)
            gameOverMenu.SetActive(true);
    }

    public void PauseMenu(GameObject pauseMenu, bool state)
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(state);
    }

    public void CreditsMenu(GameObject creditsMenu, bool state)
    {
        if (creditsMenu != null)
            creditsMenu.SetActive(state);
    }
}
