using System.Collections;
using System.Collections.Generic;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class DialogMessage : MonoBehaviour
{
    [Header("Properties")]
    public DialogProperties dialogProperties;

    [Header("Text")]
    [Tooltip("The Text in the Middle of the dialog for the current Message")]
    public string text;
    

    [Header("Options")]
    [Tooltip("The Options right next to the Text Box to continue")]
    public List<DialogOption> options;


    private List<SelfDestruct> currentlyShownObjects;
    private bool currentShownState = false;


    public void Show()
    {
        if (currentShownState)
        {
            Debug.LogError("Already Shown!", this);
            return;
        }

        if (dialogProperties == null || !dialogProperties.Validate())
        {
            Debug.LogError("No or broken DialogProperties attached!", this);
            return;
        }

        currentShownState = true;
        dialogProperties.player.LockPlayer();

        if (currentlyShownObjects.IsNullOrEmpty())
        {
            currentlyShownObjects = new List<SelfDestruct>();
        }
        else
        {
            Hide();
        }

        GameObject textObject = Instantiate(dialogProperties.textPrefab, gameObject.transform);
        RectTransform textTransform = textObject.GetComponent<RectTransform>();
        TMP_Text tmProText = textObject.GetComponentInChildren<TMP_Text>();
        SelfDestruct textDestruct = textObject.AddComponent<SelfDestruct>();

        if (textObject == null)
        {
            Debug.LogError("Cannot create text GameObject!", this);
            return;
        }

        if (textTransform == null)
        {
            Debug.LogError("No RectTransform on Prefab!", this);
            return;
        }

        if (tmProText == null)
        {
            Debug.LogError("No TMP_Text on Prefab!", this);
            return;
        }

        if (textDestruct == null)
        {
            Debug.LogError("Cannot create SelfDestruct on text!", this);
            return;
        }

        tmProText.text = text;
        textTransform.SetPositionX(dialogProperties.textPosition.x);
        textTransform.SetPositionY(dialogProperties.textPosition.y);

        currentlyShownObjects.Add(textDestruct);


        int currentNumber = 0;
        foreach (DialogOption dialogOption in options)
        {
            float yPosition = dialogProperties.optionsOrigin.y - ((dialogProperties.optionsOffset * (options.Count - 1)) / 2) + (dialogProperties.optionsOffset * (float)(currentNumber++));

            GameObject optionObject = Instantiate(dialogProperties.buttonPrefab, gameObject.transform);
            RectTransform optionTransform = optionObject.GetComponent<RectTransform>();
            TMP_Text optionText = optionObject.GetComponentInChildren<TMP_Text>();
            DialogButton dialogButton = optionObject.GetComponentInChildren<DialogButton>();
            SelfDestruct optionDestruct = optionObject.AddComponent<SelfDestruct>();

            if (optionObject == null)
                Debug.LogError("Cannot create Options GameObject!", this);
            else if (optionTransform == null)
                Debug.LogError("No RectTransform on Prefab!", this);
            else if (optionText == null)
                Debug.LogError("No TMP_Text on Prefab!", this);
            else if (dialogButton == null)
                Debug.LogError("No DialogButton on Prefab!", this);
            else if (optionDestruct == null)
                Debug.LogError("Cannot add SelfDestruct to Option!", this);
            else
            {
                dialogButton.parentMessage = this;
                dialogButton.dialogOption = dialogOption;
                optionText.text = dialogOption.optionName;
                optionTransform.SetPositionX(dialogProperties.optionsOrigin.x);
                optionTransform.SetPositionY(yPosition);

                currentlyShownObjects.Add(optionDestruct);
            }
        }
    }

    public void Hide()
    {
        if (!currentShownState)
        {
            Debug.Log("Already hidden! (1)", this);
            return;
        }

        if (dialogProperties == null || !dialogProperties.Validate())
        {
            Debug.LogError("No or broken DialogProperties attached!", this);
            return;
        }

        currentShownState = false;
        dialogProperties.player.ReleasePlayer();

        if (currentlyShownObjects.IsNullOrEmpty())
        {
            Debug.Log("Already hidden!", this);
            return;
        }

        foreach (SelfDestruct currentObject in currentlyShownObjects)
        {
            if (currentObject != null)
            {
                Debug.Log("Destruct: ", currentObject);
                currentObject.DestroyThis();
            } else
                Debug.LogError("Object already destroyed (?)!", this);
        }

        currentlyShownObjects = null;
    }
}
