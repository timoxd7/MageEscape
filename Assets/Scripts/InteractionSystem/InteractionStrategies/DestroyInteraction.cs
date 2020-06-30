public class DestroyInteraction : BaseInteraction
{
    public override void OnInteraction(PlayerContext context)
    {
        Destroy(gameObject);
    }
}