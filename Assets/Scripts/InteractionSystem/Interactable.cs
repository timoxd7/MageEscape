using InteractionSystem.InteractableObject;
using UnityEngine;

public enum InteractionBehaviour 
{
    ConsoleLog,
    Rotate,
    Empty,
}

public enum DetectionBehaviour
{
    ConsoleLog,
    Highlight,
    Empty,
}

public class Interactable : MonoBehaviour, IBaseInteractable, IInteractable, IDetectable
{

    #region Variables
    
    /* IBase */
    [Header("Properties")]
    public float holdDuration;
    public bool holdToInteract;
    public bool multipleUse;    // subject to removal
    public bool isInteractable;
    public float HoldDuration => holdDuration;
    public bool HoldToInteract => holdToInteract;
    public bool MultipleUse => multipleUse;
    public bool IsInteractable => isInteractable;

    /* Strategies */
    public IDetectable DetectionStrategy { get; set; }
    public IInteractable InteractionStrategy { get; set; }
    
    [Header("Behaviour")]
    public DetectionBehaviour detectionBehaviour;
    public InteractionBehaviour interactionBehaviour;

    #endregion

    #region Builtin
    
    protected void Start()
    {
        HandleInteractionStrategy();
        HandleDetectionStrategy();
    }
    
    #endregion

    private void HandleInteractionStrategy()
    {
        /* Subject to change
        Component c = gameObject.GetComponent<IInteractable>() as Component;
        if (c != null) Destroy(c);
        */ 
        
        switch (interactionBehaviour)
        {
            case InteractionBehaviour.ConsoleLog:
                InteractionStrategy = gameObject.AddComponent<ConsoleInteraction>();
                break;
            case InteractionBehaviour.Rotate:
                InteractionStrategy = gameObject.AddComponent<RotateInteraction>();
                break;
            case InteractionBehaviour.Empty:
                InteractionStrategy = gameObject.AddComponent<EmptyInteraction>();
                break;
            default:
                InteractionStrategy = gameObject.AddComponent<EmptyInteraction>();
                break;
        }    
    }

    private void HandleDetectionStrategy()
    {
        /* Subjet to change
        Component c = gameObject.GetComponent<IDetectable>() as Component;
        if (c != null) Destroy(c);
        */

        switch (detectionBehaviour)
        {
            case DetectionBehaviour.ConsoleLog:
                DetectionStrategy = gameObject.AddComponent<ConsoleDetection>();
                break;
            case DetectionBehaviour.Highlight:
                DetectionStrategy = gameObject.AddComponent<HighlightDetection>();
                break;
            case DetectionBehaviour.Empty:
                DetectionStrategy = gameObject.AddComponent<EmptyDetection>();
                break;
            default:
                DetectionStrategy = gameObject.AddComponent<EmptyDetection>();
                break;
        }
    }

    #region Strategy Implementation

    /* Interaction */
    public void OnInteraction()
    {
        InteractionStrategy.OnInteraction();
    }

    /* Detection */
    public void OnDetectionEnter()
    {
        DetectionStrategy.OnDetectionEnter();
    }

    public void OnDetectionExit()
    {
        DetectionStrategy.OnDetectionExit();
    }

    #endregion
}
