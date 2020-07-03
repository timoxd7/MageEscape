using UnityEngine;
using UnityEngine.Events;

public class UnityEventInteraction : BaseInteraction
{
    public UnityEvent unityEvent;

    public override void OnInteraction(PlayerContext context)
    {
        InvokeWithoutInteraction();
    }

    public void InvokeWithoutInteraction()
    {
        if (unityEvent == null)
        {
            Debug.LogError("No UnityEvent given!", this);
        } else
        {
            unityEvent.Invoke();
        }
    }
}
