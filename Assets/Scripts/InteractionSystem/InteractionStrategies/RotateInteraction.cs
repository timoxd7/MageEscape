public class RotateInteraction : BaseInteraction
{
    public override void OnInteraction(InteractionContext context)
    {
        gameObject.transform.Rotate(20, 20, 20);
    }
}
