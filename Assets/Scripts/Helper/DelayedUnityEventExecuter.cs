using UnityEngine;
using UnityEngine.Events;

public class DelayedUnityEventExecuter : MonoBehaviour
{
    public UnityEvent unityEvent;
    public float delay = 1.0f;

    private Timer delayTimer;

    // Start is called before the first frame update
    void Start()
    {
        delayTimer = new Timer();
        delayTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer.Get() >= delay)
        {
            if (unityEvent != null)
            {
                unityEvent.Invoke();
            } else
            {
                Debug.LogError("No unityEvent given!", this);
            }

            Destroy(this);
        }
    }
}
