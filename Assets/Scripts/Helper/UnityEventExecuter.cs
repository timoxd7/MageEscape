using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExecuter : MonoBehaviour
{
    public UnityEvent unityEvent;
    public bool destroyThisAfterExecution = false;
    public bool executeAtStart = false;

    private void Start()
    {
        if (executeAtStart)
        {
            Execute();
        }
    }

    public void Execute()
    {
        if (unityEvent == null)
        {
            Debug.LogError("No UnityEvent given!", this);
            return;
        }

        unityEvent.Invoke();

        if (destroyThisAfterExecution)
            Destroy(this);
    }
}
