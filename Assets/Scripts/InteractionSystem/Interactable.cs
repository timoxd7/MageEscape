using MyBox;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour, IInteractable, IDetectable
{

    #region Variables
    
    /* IBase */
    [Header("Properties")]
    public bool isInteractable;
    public bool holdToInteract;
    [ConditionalField(nameof(holdToInteract))]
    public float holdDuration;
    
    public bool IsInteractable => isInteractable;
    public bool HoldToInteract => holdToInteract;
    public float HoldDuration => holdDuration;

    [Header("Choose Behaviour")]
    [Tooltip("The DetectionOption for this Object. Choose Custom for own implementation, Custom Auto for own implementation already assigned to THIS gameObject (Custom Auto only for a single Behaviour an a gameObject)")]
    public DetectionOption detectionOption;
    [ConditionalField(nameof(detectionOption), false, DetectionOption.Custom)]
    public BaseDetection detectionBehaviour;

    [Tooltip("The InteractionOption for this Object. Choose Custom for own implementation, Custom Auto for own implementation already assigned to THIS gameObject (Custom Auto only for a single Behaviour an a gameObject)")]
    public InteractionOption interactionOption;
    [ConditionalField(nameof(interactionOption), false, InteractionOption.Custom)]
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
            case InteractionOption.Custom:
                if (interactionBehaviour == null)
                    Debug.LogError("No BaseInteraction assigned to Interactable", gameObject);
                break;
            case InteractionOption.CustomAuto:
                interactionBehaviour = gameObject.GetComponent<BaseInteraction>();
                if (interactionBehaviour == null)
                    Debug.LogError("No Script with BaseInteraction on Object to be auto added to Interactable", gameObject);
                break;
            case InteractionOption.ConsoleLog:
                interactionBehaviour = gameObject.AddComponent<ConsoleInteraction>();
                break;
            case InteractionOption.Rotate:
                interactionBehaviour = gameObject.AddComponent<RotateInteraction>();
                break;
            case InteractionOption.Empty:
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
            case DetectionOption.Custom:
                if (detectionBehaviour == null)
                    Debug.LogError("No BaseDetection assigned to Interactable", gameObject);
                break;
            case DetectionOption.CustomAuto:
                detectionBehaviour = gameObject.GetComponent<BaseDetection>();
                if (detectionBehaviour == null)
                    Debug.LogError("No Script with BaseDetection on Object to be auto added to Interactable");
                break;
            case DetectionOption.ConsoleLog:
                detectionBehaviour = gameObject.AddComponent<ConsoleDetection>();
                break;
            case DetectionOption.BasicHighlight:
                detectionBehaviour = gameObject.AddComponent<HighlightDetection>();
                break;
            case DetectionOption.Empty:
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

public enum InteractionOption 
{
    ConsoleLog,
    Rotate,
    Empty,
    Custom,
    CustomAuto
}

public enum DetectionOption
{
    ConsoleLog,
    BasicHighlight,
    Empty,
    Custom,
    CustomAuto,
}

#endregion