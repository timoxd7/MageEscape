public class InteractionData
{
    public Interactable LastInteractable { get; set; }

    public Interactable CurrentInteractable { get; set; }

    public bool InteractingNow { get; set; }

    public bool IsSame(Interactable detectable)
    {
        return CurrentInteractable == detectable;
    }

    public void Reset()
    {
        LastInteractable = CurrentInteractable;
        CurrentInteractable = null;
        InteractingNow = false;
    }

    public bool IsEmpty()
    {
        return CurrentInteractable == null;
    }
}
