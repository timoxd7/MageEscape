using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public string levelName;
    public string mainMenu;
    public GameObject optionsMenu;
    public Slider generalVolume;
    public Slider musicVolume;
    public Slider effectsVolume;

    void Start()
    {
        generalVolume.value = MainMenus.general;
        musicVolume.value = MainMenus.music;
        effectsVolume.value = MainMenus.effects;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused && optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenuUI.SetActive(true);
            } else if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        GameObject.Find("Player").GetComponent<PlayerLookController>().enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        GameObject.Find("Player").GetComponent<PlayerLookController>().enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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
