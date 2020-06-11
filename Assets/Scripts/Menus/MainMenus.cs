using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MainMenus : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public Slider generalVolume;
    public Slider musicVolume;
    public Slider effectsVolume;
    public static float general = 1f;
    public static float music;
    public static float effects;

    // Update is called once per frame
    void Update()
    {
        general = generalVolume.value;
        music = musicVolume.value;
        effects = effectsVolume.value;
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
