using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Player")]
    public PlayerAccessibility player;

    [Header("Menus")]
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;

    [Header("Sliders for Sound")]
    public Slider generalVolume;
    public static float general;
    public Slider musicVolume;
    public static float music;
    public Slider effectsVolume;
    public static float effects;

    [Header("SoundProperties")]
    public SoundProperty defaultSound;
    public SoundProperty playerSound;
    public SoundProperty environmentSound;
    public SoundProperty ambientSound;
    public SoundProperty explosionsSound;

    private bool gameIsPaused = false;

    void Start()
    {
        generalVolume.value = MainMenus.general;
        musicVolume.value = MainMenus.music;
        effectsVolume.value = MainMenus.effects;
        general = generalVolume.value;
        music = musicVolume.value;
        effects = effectsVolume.value;
    }

    void Update()
    {
        
    }

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

    public void UpdateGeneralVolume()
    {
        general = generalVolume.value;
        defaultSound.volume = general;
        playerSound.volume = general;
        environmentSound.volume = general;
        ambientSound.volume = general;
        explosionsSound.volume = general;
    }

    public void UpdateMusicVolume()
    {
        music = musicVolume.value;
        ambientSound.volume = general * music;
    }

    public void UpdateEffectVolume()
    {
        effects = effectsVolume.value;
        environmentSound.volume = general * effects;
        explosionsSound.volume = general * effects;
    }
}
