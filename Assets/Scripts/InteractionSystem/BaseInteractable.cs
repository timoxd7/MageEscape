using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{

    // Variables

    [Header("Interactable Settings")]
    public bool holdToInteract;
    [MyBox.ConditionalField("holdToInteract")]
    public float holdDuration;
    public bool mutipleUse;
    public bool isInteractable;

    // Properties

    public float HoldDuration => holdDuration;
    public bool HoldToInteract => holdToInteract;
    public bool MultipleUse => mutipleUse;
    public bool IsInteractable => isInteractable;

    public abstract void OnInteract();
}
