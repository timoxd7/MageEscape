using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Player")]
    public PlayerAccessibility player;

    [Header("Menus")]
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;

    [Header("Sound")]
    public Slider generalVolume;
    public static float general = 1f;
    public Slider musicVolume;
    public static float music = 1f;
    public Slider effectsVolume;
    public static float effects = 1f;

    [Header("Mouse Sensitivity")]
    public PlayerLookController playerLookController;
    public Slider mouseSensitivitySlider;
    public static float mouseSensitivity = 0.5f;
    public float minSensitivity = 0.1f;
    public float maxSensitivity = 2f;

    [Header("Presentation")]
    public Slider brightnessSlider;
    public PostProcessVolume postProcessVolume;
    public static float brightness = 0.333f;
    public static float minBrightness = -2f;
    public static float maxBrightness = 4f;

    [Header("SoundProperties")]
    public SoundProperty defaultSound;
    public SoundProperty playerSound;
    public SoundProperty environmentSound;
    public SoundProperty ambientSound;
    public SoundProperty explosionsSound;

    private bool gameIsPaused = false;
    private bool playerLockedBeforePause = false;
    private ColorGrading colorGrading;

    void Start()
    {
        generalVolume.value = general;
        musicVolume.value = music;
        effectsVolume.value = effects;

        UpdateGeneralVolume();
        UpdateMusicVolume();
        UpdateEffectVolume();

        mouseSensitivitySlider.value = mouseSensitivity;
        brightnessSlider.value = brightness;

        UpdateMouse();
        UpdateBrightness();
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
        if (!playerLockedBeforePause)
            player.ReleasePlayer();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        gameIsPaused = false;
    }

    public void ReturnToMainMenu()
    {
        player.ReleasePlayer();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Pause()
    {
        if (player.GetPlayerLocked())
        {
            playerLockedBeforePause = true;
        }
        else
        {
            player.LockPlayer();
            playerLockedBeforePause = false;
        }
        
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
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
        defaultSound.volume = general * effects;
        playerSound.volume = general * effects;
        environmentSound.volume = general * effects;
        ambientSound.volume = general * music;
        explosionsSound.volume = general * effects;
    }

    public void UpdateMusicVolume()
    {
        music = musicVolume.value;
        ambientSound.volume = general * music;
    }

    public void UpdateEffectVolume()
    {
        effects = effectsVolume.value;
        defaultSound.volume = general * effects;
        playerSound.volume = general * effects;
        environmentSound.volume = general * effects;
        explosionsSound.volume = general * effects;
    }

    public void UpdateMouse()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
        float newMouseSensitivity = (mouseSensitivity * (maxSensitivity - minSensitivity)) + minSensitivity;
        playerLookController.sensitivityMultiplyer = newMouseSensitivity;
    }

    public void UpdateBrightness()
    {
        if (colorGrading == null)
            postProcessVolume.profile.TryGetSettings(out colorGrading);

        brightness = brightnessSlider.value;
        ApplyBrightness(colorGrading);
    }

    public static void ApplyBrightness(ColorGrading colorGradingToApply)
    {
        float newBrightness = (brightness * (maxBrightness - minBrightness)) + minBrightness;
        colorGradingToApply.postExposure.value = newBrightness;
    }
}
