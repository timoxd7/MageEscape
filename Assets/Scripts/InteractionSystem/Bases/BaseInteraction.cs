using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour, IInteractable
{
    public abstract void OnInteraction(InteractionContext context);
}