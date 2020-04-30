namespace InteractionSystem.InteractableObject
{
    public interface IBaseInteractable
    {
        float HoldDuration { get; }
        bool HoldToInteract { get; } 
        bool MultipleUse { get; }
        bool IsInteractable { get; }
        
        
        IDetectable DetectionStrategy { get; set; }
        IInteractable InteractionStrategy { get; set; }
    }
}