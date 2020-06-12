using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public PlayerAccessibility player;
    public GameObject pauseMenuUI;
    public string levelName;
    public string mainMenu;
    public GameObject optionsMenu;
    public Slider generalVolume;
    public Slider musicVolume;
    public Slider effectsVolume;

    private bool gameIsPaused = false;

    void Start()
    {
        generalVolume.value = MainMenus.general;
        musicVolume.value = MainMenus.music;
        effectsVolume.value = MainMenus.effects;
    }

    // Update is called once per frame
    public void PausePressed()
    {
        if(gameIsPaused && optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            pauseMenuUI.SetActive(true);
        } else if(gameIsPaused)
        {
            Resume();
        } else
        {
            Pause();
        }
    }

    public void Resume()
    {
        player.ReleasePlayer();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        player.LockPlayer();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Options() 
    {
        optionsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
