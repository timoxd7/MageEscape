using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjectAddForceInteraction : AddForceInteraction
{
    public DisableInteractableOnTrigger interactionDisablerToRegister;

    private void Start()
    {
        Interactable interactable = gameObject.GetOrAddComponent<Interactable>();
        HighlightDetection highlightDetection = gameObject.GetOrAddComponent<HighlightDetection>();

        interactable.isInteractable = true;
        interactable.holdToInteract = false;
        interactable.itemRequired = false;
        interactable.interactionOption = InteractionOption.Custom;
        interactable.interactionBehaviour = this;
        interactable.detectionOption = DetectionOption.Custom;
        interactable.detectionBehaviour = highlightDetection;

        if (interactionDisablerToRegister != null)
        {
            if (interactionDisablerToRegister.interactables.IsNullOrEmpty())
                interactionDisablerToRegister.interactables = new List<UnityEngine.GameObject>();

            interactionDisablerToRegister.interactables.Add(gameObject);
        } else
        {
            Debug.LogError("No interactionDisabler given!", this);
        }
    }
}
