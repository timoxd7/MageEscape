public interface IInteractable
{
    
    float HoldDuration { get; }

    bool HoldToInteract { get; }

    bool MultipleUse { get; }

    bool IsInteractable{ get; }

    void OnInteract();
}

