using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExecuter : MonoBehaviour
{
    public UnityEvent unityEvent;
    public bool destroyThisAfterExecution = false;
    public bool executeAtStart = false;
    public bool delay = false;
    [ConditionalField(nameof(delay))]
    public float delayTime = 1.0f;

    private void Start()
    {
        if (executeAtStart)
        {
            Execute();
        }
    }

    public void Execute()
    {
        if (delay)
        {
            if (!CheckEventReference())
                return;

            DelayedUnityEventExecuter delayedExecuter = gameObject.AddComponent<DelayedUnityEventExecuter>();
            delayedExecuter.unityEvent = unityEvent;
            delayedExecuter.delay = delayTime;
            
            if (destroyThisAfterExecution)
                Destroy(this);
        } else
        {
            Run();
        }
    }

    private void Run()
    {
        if (!CheckEventReference())
            return;

        unityEvent.Invoke();

        if (destroyThisAfterExecution)
            Destroy(this);
    }

    public bool CheckEventReference()
    {
        if (unityEvent == null)
        {
            Debug.LogError("No UnityEvent given!", this);
            return false;
        }

        return true;
    }
}
