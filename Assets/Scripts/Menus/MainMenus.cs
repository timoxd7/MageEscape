using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MainMenus : MonoBehaviour
{
    [Header("Volume")]
    public Slider generalVolume;
    public Slider musicVolume;
    public Slider effectsVolume;

    [Header("Presentation")]
    public Slider mouseSensitivity;
    public Slider brightness;
    public PostProcessVolume postProcessVolume;

    [Header("Application")]
    public GameObject exitMenu;

    private ColorGrading colorGrading = null;

    // Update is called once per frame
    void Start()
    {
        generalVolume.value = PauseMenu.general;
        musicVolume.value = PauseMenu.music;
        effectsVolume.value = PauseMenu.effects;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            exitMenu.SetActive(false);
        }

        mouseSensitivity.value = PauseMenu.mouseSensitivity;
        brightness.value = PauseMenu.brightness;
        UpdateBrightness();
    }

    public void UpdateGeneral()
    {
        PauseMenu.general = generalVolume.value;
    }

    public void UpdateMusic()
    {
        PauseMenu.music = musicVolume.value;
    }

    public void UpdateEffects()
    {
        PauseMenu.effects = effectsVolume.value;
    }

    public void UpdateMouse()
    {
        PauseMenu.mouseSensitivity = mouseSensitivity.value;
    }

    public void UpdateBrightness()
    {
        PauseMenu.brightness = brightness.value;
        ApplyBrightness();
    }

    private void ApplyBrightness()
    {
        if (colorGrading == null)
            postProcessVolume.profile.TryGetSettings(out colorGrading);

        PauseMenu.ApplyBrightness(colorGrading);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
