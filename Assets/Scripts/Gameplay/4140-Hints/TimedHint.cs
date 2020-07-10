using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class TimedHint : MonoBehaviour
{
    [Tooltip("The time until the Hint gets displayed in seconds")]
    public float timeForHint = 300f;
    public DialogMessage hintMessage;

    public bool executeUnityEventOnDone = false;
    [ConditionalField(nameof(executeUnityEventOnDone))]
    public UnityEvent unityEvent;

    private bool riddleEntered = false;
    private Timer timeSinceEnter;

    void Update()
    {
        if (riddleEntered)
        {
            if (timeSinceEnter.Get() >= timeForHint)
            {
                if (!DialogMessage.AnyMessageShown())
                {
                    if (hintMessage != null)
                    {
                        hintMessage.Show();
                    } else
                    {
                        Debug.LogError("No hint given!", this);
                    }

                    Destroy(this);
                } else
                {
                    Debug.Log("Showing other Message");
                }
            }
        }
    }

    public void RiddleStart()
    {
        if (riddleEntered)
            return;

        timeSinceEnter = new Timer();
        timeSinceEnter.Start();

        riddleEntered = true;
    }

    public void RiddleDone()
    {
        riddleEntered = false;

        if (executeUnityEventOnDone)
            if (unityEvent != null)
                unityEvent.Invoke();

        Destroy(this);
    }
}
