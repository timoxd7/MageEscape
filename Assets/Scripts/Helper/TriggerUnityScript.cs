using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TriggerUnityScript : MonoBehaviour
{
    public UnityEvent unityEvent;
    public string objectTag = "Player";
    public bool onlyOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTag)
        {
            if (unityEvent != null)
                unityEvent.Invoke();
            else
                Debug.LogError("No unityEvent given!", this);

            if (onlyOnce)
                Destroy(this);
        }
    }
}
