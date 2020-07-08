using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class DialogOption : MonoBehaviour
{
    public string optionName;
    public EventType eventType;

    [ConditionalField(nameof(eventType), false, EventType.UnityEvent)]
    public UnityEvent unityEvent;

    [ConditionalField(nameof(eventType), false, EventType.DialogMessage)]
    public DialogMessage dialogMessage;


    public void Execute(DialogMessage parentMessage = null)
    {
        if (eventType == EventType.None)
        {
            Debug.LogWarning("No execution context given!", this);
            return;
        }

        if (parentMessage != null)
            parentMessage.Hide();

        switch (eventType)
        {
            case EventType.Close:
                // Will be closed automatically
                break;

            case EventType.UnityEvent:
                if (unityEvent == null)
                    Debug.LogError("No UnityEvent given!", this);
                else
                    unityEvent.Invoke();
                break;

            case EventType.DialogMessage:
                if (dialogMessage == null)
                    Debug.LogError("No dialogMessage given!", this);
                else
                    dialogMessage.Show();
                break;
        }
    }

    public enum EventType
    {
        None,
        Close,
        UnityEvent,
        DialogMessage
    }
}
