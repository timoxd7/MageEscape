public class InteractableObjectExample : BaseInteractable
{
    public override void OnInteract()
    {
        gameObject.transform.Rotate(20f, 20f, 20f);
    }
}
