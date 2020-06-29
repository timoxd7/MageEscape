using MyBox;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumpadController : MonoBehaviour
{
    public GameObject keyToActivate;
    public List<NumpadButton> keypadButtons;
    public Text numpadDisplayText;
    public Animator doorAnimator;
    public string doorOpenTrigger;
    public string keyCode;

    private string currentInput = "";
    private const int maxDigits = 6;
    private bool opened = false;

    public void Awake()
    {
        if (keyCode.Length > maxDigits)
        {
            Debug.LogError("Too long Key given!", this);
            return;
        }

        for (int i = 0; i < keyCode.Length; i++)
        {
            if (!IsNumerical(keyCode[i]))
            {
                Debug.LogError("No Numerical Key!", this);
            }
        }
    }

    public void ButtonPressed(NumpadButtonType button)
    {
        if (opened)
            return;

        if ((int)button < 10)
        {
            if (currentInput.Length >= maxDigits)
            {
                ButtonPressed(NumpadButtonType.Cancel);
            }

            currentInput += ((int)button) + "";
            numpadDisplayText.text = currentInput;
        } else
        {
            switch (button)
            {
                case NumpadButtonType.Enter:
                    if (currentInput == keyCode)
                    {
                        numpadDisplayText.text = "Unlock";
                        doorAnimator.SetTrigger(doorOpenTrigger);
                        opened = true;
                        keyToActivate.SetActive(true);
                        DeactivateButtons();
                    } else
                    {
                        currentInput = "";
                        numpadDisplayText.text = "Wrong";
                    }
                    break;
                case NumpadButtonType.Cancel:
                    currentInput = "";
                    numpadDisplayText.text = "";
                    break;
            }
        }
    }

    private bool IsNumerical(char mayNumerical)
    {
        switch (mayNumerical)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return true;
        }

        return false;
    }

    private void DeactivateButtons()
    {
        if (keypadButtons.IsNullOrEmpty())
            return;

        foreach(NumpadButton button in keypadButtons)
        {
            button.Deactivate();
        }
    }

    public enum NumpadButtonType : int
    {
        N0 = 0,
        N1 = 1,
        N2 = 2,
        N3 = 3,
        N4 = 4,
        N5 = 5,
        N6 = 6,
        N7 = 7,
        N8 = 8,
        N9 = 9,
        Cancel = 10,
        Enter = 11
    }
}
