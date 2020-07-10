using MyBox;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DialogFixToolkit : MonoBehaviour
{
#if UNITY_EDITOR

    [Header("Settings")]
    public string labelToSearchFor = "Close";
    public string newLabel = "Schließen";
    public bool destroyGameobjectOnAwake = true;
    [ConditionalField(nameof(destroyGameobjectOnAwake), true)]
    public bool destroyThisOnAwake = true;

    [Header("Only for Info!")]
    public int foundInstances;
    public int foundCloses;
    public int replacedLabels;

    [Header("Result Arrays")]
    public List<DialogOption> foundOptions;
    public List<DialogMessage> foundMessages;

    private void Awake()
    {
        if (!Application.isPlaying)
            return;

        if (destroyGameobjectOnAwake)
        {
            Destroy(gameObject);
        } else if (destroyThisOnAwake)
        {
            Destroy(this);
        }
    }

    [ButtonMethod]
    public void ReplaceCloseLabels()
    {
        foundOptions = new List<DialogOption>();

        DialogOption[] dialogOptions = FindObjectsOfType<DialogOption>();
        foundInstances = dialogOptions.Length;

        foundCloses = 0;
        replacedLabels = 0;

        foreach(DialogOption dialogOption in dialogOptions)
        {
            if (dialogOption.eventType == DialogOption.EventType.Close)
            {
                foundCloses++;

                if (dialogOption.optionName.Equals(labelToSearchFor))
                {
                    Undo.RecordObject(dialogOption, "Rename Close Option Name");
                    dialogOption.optionName = newLabel;
                    replacedLabels++;
                    foundOptions.Add(dialogOption);
                }
            }
        }
    }

    [ButtonMethod]
    public void SearchOnlyWrongCloseLabels()
    {
        foundOptions = new List<DialogOption>();

        DialogOption[] dialogOptions = FindObjectsOfType<DialogOption>();
        foundInstances = dialogOptions.Length;

        foundCloses = 0;
        replacedLabels = 0;

        foreach (DialogOption dialogOption in dialogOptions)
        {
            if (dialogOption.eventType == DialogOption.EventType.Close)
            {
                foundCloses++;

                if (dialogOption.optionName.Equals(labelToSearchFor))
                {
                    replacedLabels++;
                    foundOptions.Add(dialogOption);
                }
            }
        }
    }

    [ButtonMethod]
    public void FindAllMessagesWithFirstLowercase()
    {
        foundMessages = new List<DialogMessage>();

        DialogMessage[] dialogMessages = FindObjectsOfType<DialogMessage>();
        foundInstances = dialogMessages.Length;

        foreach (DialogMessage dialogMessage in dialogMessages)
        {
            if (dialogMessage.text != "") {
                if (char.IsLower(dialogMessage.text[0])) {
                    foundMessages.Add(dialogMessage);
                }
            }
        }
    }
#endif
}
