using System.Collections.Generic;
using MyBox;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DialogMessage : MonoBehaviour
{
    [Header("Properties")]
    public bool autoAssignGlobalProperties = true;
    [ConditionalField(nameof(autoAssignGlobalProperties))]
    public string dialogPropertiesTag = "GlobalDialogProperties";
    [ConditionalField(nameof(autoAssignGlobalProperties), true)]
    public DialogProperties dialogProperties;

    [Header("Text")]
    [Tooltip("The Text in the Middle of the dialog for the current Message")]
    public string text;
    public string nextOptionText = "Weiter";
    public string backOptionText = "Zurück";
    public string closeOptionText = "Schließen";
    

    [Header("Options")]
    [Tooltip("The Options right next to the Text Box to continue")]
    public List<DialogOption> options;


    [Header("Sound Play")]
    public bool playSoundOnShow = false;
    [ConditionalField(nameof(playSoundOnShow))]
    public SoundSourcePlayer soundSourcePlayer;

    [Header("Untiy Event")]
    public bool triggerUnityEventOnShow = false;
    [ConditionalField(nameof(triggerUnityEventOnShow))]
    public UnityEvent unityEvent;


#if UNITY_EDITOR
    [Header("Next Message Automation")]
    public bool autoAssignNext = true;
    [ConditionalField(nameof(autoAssignNext))]
    public bool autoAddNext = true;
    public bool autoAddNextOnNext = true;
    public bool autoAddPreviousOnNext = true;
    public bool autoAddCloseOnNext = true;
#endif


    private List<SelfDestruct> currentlyShownObjects;
    private bool currentShownState = false;
    private static List<DialogMessage> currentlyShownMessages;


    [ButtonMethod]
    public void Show()
    {
        if (currentShownState)
        {
            Debug.LogError("Already Shown!", this);
            return;
        }

        if (autoAssignGlobalProperties)
            GetProperties();

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

        if (currentlyShownMessages == null)
            currentlyShownMessages = new List<DialogMessage>();

        currentlyShownMessages.Add(this);

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

        if (playSoundOnShow && soundSourcePlayer != null)
            soundSourcePlayer.Play();

        if (triggerUnityEventOnShow && unityEvent != null)
            unityEvent.Invoke();
    }

    [ButtonMethod]
    public void Hide()
    {
        if (!currentShownState)
        {
            Debug.Log("Already hidden! (1)", this);
            return;
        }

        if (autoAssignGlobalProperties)
            GetProperties();

        if (dialogProperties == null || !dialogProperties.Validate())
        {
            Debug.LogError("No or broken DialogProperties attached!", this);
        } else
        {
            dialogProperties.player.ReleasePlayer();
        }

        currentShownState = false;

        if (currentlyShownMessages.IsNullOrEmpty())
        {
            Debug.LogError("Message not in shown messages (?)", this);
        } else if (currentlyShownMessages.Contains(this))
        {
            currentlyShownMessages.Remove(this);
        } else
        {
            Debug.LogError("Message not in shown messages (?) (2)", this);
        }

        if (currentlyShownObjects.IsNullOrEmpty())
        {
            Debug.Log("Already hidden!", this);
            return;
        }

        foreach (SelfDestruct currentObject in currentlyShownObjects)
        {
            if (currentObject != null)
            {
                //Debug.Log("Destruct: ", currentObject);
                currentObject.DestroyThis();
            } else
                Debug.LogError("Object already destroyed (?)!", this);
        }

        currentlyShownObjects = null;
    }

    public static bool AnyMessageShown()
    {
        return !currentlyShownMessages.IsNullOrEmpty();
    }

    public void OnDisable()
    {
        if (currentShownState)
            Hide();
    }

    public void OnDestroy()
    {
        if (currentShownState)
            Hide();
    }

    private void GetProperties()
    {
        GameObject dialogPropertiesObject = GameObject.FindGameObjectWithTag(dialogPropertiesTag);
        if (dialogPropertiesObject != null)
        {
            dialogProperties = dialogPropertiesObject.GetComponent<DialogProperties>();
        }
    }

#if UNITY_EDITOR
    [ButtonMethod]
    [ExecuteInEditMode]
    public void AddMessageBehind()
    {
        GameObject emptyObject = new GameObject();
        GameObject nextMessageObject = Instantiate(emptyObject, transform.parent);
        DestroyImmediate(emptyObject);

        Undo.RecordObject(nextMessageObject, "Change Message Name");
        nextMessageObject.name = "NewNextMessage";
        Undo.RecordObject(nextMessageObject, "Add Options");
        DialogMessage nextMessage = nextMessageObject.AddComponent<DialogMessage>();
        if (nextMessage.options.IsNullOrEmpty())
            nextMessage.options = new List<DialogOption>();

        if (autoAddNextOnNext)
        {
            // Add Close, Previous and Next to new Messag
            DialogOption nextOption = nextMessageObject.AddComponent<DialogOption>();
            nextOption.optionName = nextOptionText;
            nextOption.eventType = DialogOption.EventType.DialogMessage;
            nextMessage.options.Add(nextOption);
        }

        if (autoAddPreviousOnNext)
        {
            DialogOption backOption = nextMessageObject.AddComponent<DialogOption>();
            backOption.optionName = backOptionText;
            backOption.eventType = DialogOption.EventType.DialogMessage;
            backOption.dialogMessage = this;
            nextMessage.options.Add(backOption);
        }

        if (autoAddCloseOnNext)
        {
            DialogOption closeOption = nextMessageObject.AddComponent<DialogOption>();
            closeOption.optionName = closeOptionText;
            closeOption.eventType = DialogOption.EventType.Close;
            nextMessage.options.Add(closeOption);
        }

        Undo.RecordObject(nextMessage, "Autoassing Values");
        nextMessage.autoAssignNext = autoAssignNext;
        nextMessage.autoAddNext = autoAddNext;
        nextMessage.autoAddNextOnNext = autoAddNextOnNext;
        nextMessage.autoAddPreviousOnNext = autoAddPreviousOnNext;
        nextMessage.autoAddCloseOnNext = autoAddCloseOnNext;

        
        // Add message to this next option
        DialogOption[] possiblyNext = gameObject.GetComponents<DialogOption>();

        if (autoAssignNext)
        {
            Undo.RecordObject(this, "Add Next reference");

            bool nextOptionFound = false;
            if (!possiblyNext.IsNullOrEmpty())
            {
                for (int i = 0; i < possiblyNext.Length; i++)
                {
                    if (possiblyNext[i].optionName == nextOptionText)
                    {
                        possiblyNext[i].eventType = DialogOption.EventType.DialogMessage;
                        possiblyNext[i].dialogMessage = nextMessage;
                        nextOptionFound = true;
                    }
                }
            }

            if (autoAddNext)
            {
                if (!nextOptionFound)
                {
                    DialogOption thisNextOption = gameObject.AddComponent<DialogOption>();
                    thisNextOption.optionName = nextOptionText;
                    thisNextOption.eventType = DialogOption.EventType.DialogMessage;
                    thisNextOption.dialogMessage = nextMessage;

                    if (options.IsNullOrEmpty())
                        options = new List<DialogOption>();

                    options.Insert(0, thisNextOption);
                }
            }
        }
    }

    [ButtonMethod]
    [ExecuteInEditMode]
    public void ApplyTitleAsText()
    {
        Undo.RecordObject(this, "Applying title as text");
        text = gameObject.name;
    }

    [ButtonMethod]
    [ExecuteInEditMode]
    public void AddAllOptions()
    {
        Undo.RecordObject(this, "Adding all Options");

        DialogOption[] foundOptions = gameObject.GetComponents<DialogOption>();

        int oldOptionsCount = options.Count;
        int arrayLength = foundOptions.Length;
        for (int i = oldOptionsCount; i < arrayLength + oldOptionsCount; i++)
        {
            options.Add(foundOptions[i]);
        }
    }

    [ButtonMethod]
    [ExecuteInEditMode]
    public void RemoveAllOptionReferences()
    {
        if (!options.IsNullOrEmpty())
        {
            Undo.RecordObject(this, "Removing all Option References");
            options.Clear();
        }
    }

    [ButtonMethod]
    [ExecuteInEditMode]
    public void RemoveAllOptions()
    {
        RemoveAllOptionReferences();

        DialogOption[] optionsToDelete = gameObject.GetComponents<DialogOption>();

        if (!optionsToDelete.IsNullOrEmpty())
        {
            Undo.RecordObject(this, "Removing all Options");

            for (int i = 0; i < optionsToDelete.Length; i++)
            {
                DestroyImmediate(optionsToDelete[i]);
            }
        }
    }

    [ButtonMethod]
    [ExecuteInEditMode]
    public void AddNextOption()
    {
        Undo.RecordObject(this, "Autoadd next Option");

        DialogOption nextOption = gameObject.AddComponent<DialogOption>();

        nextOption.optionName = nextOptionText;
        nextOption.eventType = DialogOption.EventType.DialogMessage;

        if (options.IsNullOrEmpty())
            options = new List<DialogOption>();

        options.Add(nextOption);
    }

    [ButtonMethod]
    [ExecuteInEditMode]
    public void AddCloseOption()
    {
        Undo.RecordObject(this, "Autoadd close Option");

        DialogOption closeOption = gameObject.AddComponent<DialogOption>();

        closeOption.optionName = closeOptionText;
        closeOption.eventType = DialogOption.EventType.Close;

        if (options.IsNullOrEmpty())
            options = new List<DialogOption>();

        options.Add(closeOption);
    }
#endif
}
