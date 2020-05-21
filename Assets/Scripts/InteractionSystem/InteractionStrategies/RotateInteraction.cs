public class RotateInteraction : BaseInteraction
{
    public override void OnInteraction(PlayerContext context)
    {
        gameObject.transform.Rotate(20, 20, 20);
    }
}
