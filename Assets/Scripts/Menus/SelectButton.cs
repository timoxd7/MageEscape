using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Button lowbutton;
    public Button middlebutton;
    public Button highbutton;
    public Color orgColor;

    public void lowbuttonClicked()
    {
        QualitySettings.DecreaseLevel(true);
        lowbutton.GetComponent<Image>().color = Color.red;
        middlebutton.image.color = orgColor;
        highbutton.image.color = orgColor;
    }

    public void middlebuttonClicked()
    {
        middlebutton.GetComponent<Image>().color = Color.red;
        lowbutton.image.color = orgColor;
        highbutton.image.color = orgColor;
    }

    public void highbuttonClicked()
    {
        QualitySettings.IncreaseLevel(true);
        highbutton.GetComponent<Image>().color = Color.red;
        middlebutton.image.color = orgColor;
        lowbutton.image.color = orgColor;
    }
}
