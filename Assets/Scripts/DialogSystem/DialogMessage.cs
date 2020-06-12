using System.Collections;
using System.Collections.Generic;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class DialogMessage : MonoBehaviour
{
    [ConditionalField(nameof(useFromPreviousMessage), true)]
    public GameObject textPrefab;
    [ConditionalField(nameof(useFromPreviousMessage), true)]
    public Vector2 textPosition;
    [ConditionalField(nameof(useFromPreviousMessage), true)]
    public GameObject buttonPrefab;
    [ConditionalField(nameof(useFromPreviousMessage), true)]
    public Vector2 optionsOrigin;
    [ConditionalField(nameof(useFromPreviousMessage), true)]
    public float optionsOffset;
    [ConditionalField(nameof(useFromPreviousMessage), true)]
    public PlayerAccessibility player;

    public bool useFromPreviousMessage = false;

    [Header("Text")]
    public string text;
    

    [Header("Options")]
    public List<DialogOption> options;


    private List<SelfDestruct> currentlyShownObjects;
    private bool currentShownState = false;


    public void Show()
    {
        if (useFromPreviousMessage)
        {
            Debug.LogError("Should use Settings from previous Dialog, but this Dialog is called as the first dialog! This is not possible!", this);
            return;
        }

        PrivateShow();
    }

    public void Show(DialogMessage previous)
    {
        if (useFromPreviousMessage)
        {
            textPrefab = previous.textPrefab;
            textPosition = previous.textPosition;
            buttonPrefab = previous.buttonPrefab;
            optionsOrigin = previous.optionsOrigin;
            optionsOffset = previous.optionsOffset;
            player = previous.player;
        }

        PrivateShow();
    }

    public void Hide()
    {
        if (!currentShownState)
        {
            Debug.Log("Already hidden! (1)", this);
            return;
        }

        if (player == null)
        {
            Debug.LogError("No Player attached! (1)", this);
            return;
        }

        currentShownState = false;
        player.ReleasePlayer();

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

    private void PrivateShow()
    {
        if (currentShownState)
        {
            Debug.LogError("Already Shown!", this);
            return;
        }

        if (player == null)
        {
            Debug.LogError("No Player attached!", this);
            return;
        }

        currentShownState = true;
        player.LockPlayer();

        if (currentlyShownObjects.IsNullOrEmpty())
        {
            currentlyShownObjects = new List<SelfDestruct>();
        }
        else
        {
            Hide();
        }

        GameObject textObject = Instantiate(textPrefab, gameObject.transform);
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
        textTransform.SetPositionX(textPosition.x);
        textTransform.SetPositionY(textPosition.y);

        currentlyShownObjects.Add(textDestruct);


        int currentNumber = 0;
        foreach (DialogOption dialogOption in options)
        {
            float yPosition = optionsOrigin.y - ((optionsOffset * (options.Count - 1)) / 2) + (optionsOffset * (float)(currentNumber++));

            GameObject optionObject = Instantiate(buttonPrefab, gameObject.transform);
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
                optionTransform.SetPositionX(optionsOrigin.x);
                optionTransform.SetPositionY(yPosition);

                currentlyShownObjects.Add(optionDestruct);
            }
        }
    }
}
