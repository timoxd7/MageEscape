using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{

    // Variables

    [Header("Interactable Settings")]
    public float holdDuration;
    public bool holdToInteract;
    public bool mutipleUse;
    public bool isInteractable;

    // Properties

    public float HoldDuration => holdDuration;
    public bool HoldToInteract => holdToInteract;
    public bool MultipleUse => mutipleUse;
    public bool IsInteractable => isInteractable;

    // Methods

    public void OnInteract()
    {
        Debug.Log("INTERACTED: " + gameObject.name);
    }
}
