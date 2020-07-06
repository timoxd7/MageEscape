using MyBox;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour, IInteractable, IDetectable
{

    #region Variables
    
    /* IBase */
    [Header("Properties")]
    public bool isInteractable = true;
    public bool holdToInteract = false;
    [ConditionalField(nameof(holdToInteract))]
    public float holdDuration = 0.5f;
    public bool itemRequired = false;
    [ConditionalField(nameof(itemRequired))]
    [Tooltip("The unique Id of the item required to use this interactable")]
    public string itemId;
    
    public bool IsInteractable => isInteractable;
    public bool HoldToInteract => holdToInteract;
    public float HoldDuration => holdDuration;
    public bool ItemRequired => itemRequired;
    public string ItemId => itemId;
    
    [Header("Choose Behaviour")]
    [Tooltip("The DetectionOption for this Object. Choose Custom for own implementation, Custom Auto for own implementation already assigned to THIS gameObject (Custom Auto only for a single Behaviour an a gameObject)")]
    public DetectionOption detectionOption;
    [ConditionalField(nameof(detectionOption), false, DetectionOption.Custom)]
    public BaseDetection detectionBehaviour;

    [Tooltip("The InteractionOption for this Object. Choose Custom for own implementation, Custom Auto for own implementation already assigned to THIS gameObject (Custom Auto only for a single Behaviour an a gameObject)")]
    public InteractionOption interactionOption;
    [ConditionalField(nameof(interactionOption), false, InteractionOption.Custom)]
    public BaseInteraction interactionBehaviour;
    [ConditionalField(nameof(itemRequired))]
    public bool addNoRequiredItemInteraction = false;
    [ConditionalField(nameof(addNoRequiredItemInteraction))]
    public BaseInteraction noItemInteractionBehaviour;

    private bool lastRealDetectionState = false;
    private bool lastNoticedDetectionState = false;

    #endregion

    #region Builtin
    
    protected void Start()
    {
        // Automatische Layerzuweisung.
        // TODO: 3567
        gameObject.layer = 8;
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
                if (noItemInteractionBehaviour == null && addNoRequiredItemInteraction)
                    Debug.LogError("No BaseInteraction for no Item assigned to Interactable", gameObject);
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

    public void OnInteraction(PlayerContext context)
    {
        if (!enabled)
        {
            return;
        }

        if (ItemRequired)
        {
            Item required = context.inventory.ContainsItem(ItemId);
            if (required != null)
            {
                // Achtung, hier ist die Reihenfolge wichtig:
                // Erst das Item aus dem Inventar entfernen, und dann die Interaction!x
                if (required.Consumable)
                {
                    context.inventory.RemoveItem(required.UniqeId);
                }
                interactionBehaviour.OnInteraction(context);
            } 
            else if (addNoRequiredItemInteraction)
            {
                noItemInteractionBehaviour.OnInteraction(context);
            }
        }
        else
        {
            interactionBehaviour.OnInteraction(context);
        }
    }

    public void OnDetectionEnter()
    {
        lastNoticedDetectionState = true;

        if (!enabled)
        {
            return;
        }

        detectionBehaviour.OnDetectionEnter();
        lastRealDetectionState = true;
    }

    public void OnDetectionExit()
    {
        lastNoticedDetectionState = false;

        if (!enabled)
        {
            return;
        }

        detectionBehaviour.OnDetectionExit();
        lastRealDetectionState = false;
    }

    private void OnEnable()
    {
        if (lastNoticedDetectionState)
        {
            detectionBehaviour.OnDetectionEnter();
            lastRealDetectionState = true;
        }
    }

    public void OnDisable()
    {
        if (lastRealDetectionState)
        {
            detectionBehaviour.OnDetectionExit();
            lastRealDetectionState = false;
        }
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
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