using UnityEngine;

/**
 * Hiermit kommunizieren wir mit dem Interface IIteractable
 */


[CreateAssetMenu(fileName = "InteractionData", menuName = "InteractionSystem/InteractionData")]
public class InteractionData : ScriptableObject
{
    private BaseInteractable interactable;

    public BaseInteractable Interactable
    {
        get => interactable;
        set => interactable = value;
    }

    public void Interact()
    {
        interactable.OnInteract();
    }

    public bool IsSameInteractable(BaseInteractable newInteractable)
    {
        return interactable == newInteractable;
    }

    public void ResetData()
    {
        interactable = null;
    }

    public bool isEmpty()
    {
        return interactable == null;
    }
}
