using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MainMenus : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public static Slider generalVolume;
    public static Slider musicVolume;
    public static Slider effectsVolume;

    // Update is called once per frame
    void Update()
    {
        /*if(isActiveAndEnabled)
        {
            generalVolume.value = PauseMenu.generalVolume.value;
            musicVolume.value = PauseMenu.musicVolume.value;
            effectsVolume.value = PauseMenu.effectsVolume.value;
        }*/
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

}
