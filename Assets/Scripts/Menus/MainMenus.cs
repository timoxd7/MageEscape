using UnityEngine;
using UnityEngine.UI;

public class MainMenus : MonoBehaviour
{
    public Slider generalVolume;
    public Slider musicVolume;
    public Slider effectsVolume;

    // Update is called once per frame
    void Start()
    {
        generalVolume.value = PauseMenu.general;
        musicVolume.value = PauseMenu.music;
        effectsVolume.value = PauseMenu.effects;
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
}
