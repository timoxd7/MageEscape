using MyBox;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour, IInteractable, IDetectable
{

    #region Variables
    
    /* IBase */
    [Header("Properties")]
    // public float holdDuration;
    public bool isInteractable;
    public bool holdToInteract;
    [ConditionalField(nameof(holdToInteract))]
    public float holdDuration;
    // public bool multipleUse;    // subject to removal

    
    public bool IsInteractable => isInteractable;
    public bool HoldToInteract => holdToInteract;
    public float HoldDuration => holdDuration;
    // public bool MultipleUse => multipleUse;

    [Header("Choose Behaviour")]
    [Tooltip("The DetectionBehaviour for this Object. Choose Custom for own implementation, Custom Auto for own implementation already assigned to THIS gameObject (Custom Auto only for a single Behaviour an a gameObject)")]
    public DetectionBehaviour detectionOption;
    [ConditionalField(nameof(detectionOption), false, DetectionBehaviour.Custom)]
    public BaseDetection detectionBehaviour;

    [Tooltip("The InteractionBehaviour for this Object. Choose Custom for own implementation, Custom Auto for own implementation already assigned to THIS gameObject (Custom Auto only for a single Behaviour an a gameObject)")]
    public InteractionBehaviour interactionOption;
    [ConditionalField(nameof(interactionOption), false, InteractionBehaviour.Custom)]
    public BaseInteraction interactionBehaviour;

    #endregion

    #region Builtin
    
    protected void Start()
    {
        HandleInteractionStrategy();
        HandleDetectionStrategy();
    }
    
    #endregion

    #region Strategy Handler
    
    private void HandleInteractionStrategy()
    {
        switch (interactionOption)
        {
            case InteractionBehaviour.Custom:
                if (interactionBehaviour == null)
                    Debug.LogError("No BaseInteraction assigned to Interactable", gameObject);
                break;
            case InteractionBehaviour.CustomAuto:
                interactionBehaviour = gameObject.GetComponent<BaseInteraction>();
                if (interactionBehaviour == null)
                    Debug.LogError("No Script with BaseInteraction on Object to be auto added to Interactable", gameObject);
                break;
            case InteractionBehaviour.ConsoleLog:
                interactionBehaviour = gameObject.AddComponent<ConsoleInteraction>();
                break;
            case InteractionBehaviour.Rotate:
                interactionBehaviour = gameObject.AddComponent<RotateInteraction>();
                break;
            case InteractionBehaviour.Empty:
                interactionBehaviour = gameObject.AddComponent<EmptyInteraction>();
                break;
            default:
                interactionBehaviour = gameObject.AddComponent<EmptyInteraction>();
                break;
        }    
    }

    private void HandleDetectionStrategy()
    {
        switch (detectionOption)
        {
            case DetectionBehaviour.Custom:
                if (detectionBehaviour == null)
                    Debug.LogError("No BaseDetection assigned to Interactable", gameObject);
                break;
            case DetectionBehaviour.CustomAuto:
                detectionBehaviour = gameObject.GetComponent<BaseDetection>();
                if (detectionBehaviour == null)
                    Debug.LogError("No Script with BaseDetection on Object to be auto added to Interactable");
                break;
            case DetectionBehaviour.ConsoleLog:
                detectionBehaviour = gameObject.AddComponent<ConsoleDetection>();
                break;
            case DetectionBehaviour.BasicHighlight:
                detectionBehaviour = gameObject.AddComponent<HighlightDetection>();
                break;
            case DetectionBehaviour.Empty:
                detectionBehaviour = gameObject.AddComponent<EmptyDetection>();
                break;
            default:
                detectionBehaviour = gameObject.AddComponent<EmptyDetection>();
                break;
        }
    }
    
    #endregion

    #region Strategy Implementation

    public void OnInteraction(InteractionContext context)
    {
        interactionBehaviour.OnInteraction(context);
    }

    public void OnDetectionEnter()
    {
        detectionBehaviour.OnDetectionEnter();
    }

    public void OnDetectionExit()
    {
        detectionBehaviour.OnDetectionExit();
    }

    #endregion
}

#region Enums

public enum InteractionBehaviour 
{
    ConsoleLog,
    Rotate,
    Empty,
    Custom,
    CustomAuto
}

public enum DetectionBehaviour
{
    ConsoleLog,
    BasicHighlight,
    Empty,
    Custom,
    CustomAuto,
}

#endregion