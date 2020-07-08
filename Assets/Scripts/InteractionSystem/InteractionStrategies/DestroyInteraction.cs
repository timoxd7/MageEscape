using MyBox;
public class DestroyInteraction : BaseInteraction
{
    public bool playSound = false;
    [ConditionalField(nameof(playSound))]
    public SoundSourcePlayer soundSourcePlayer;

    public override void OnInteraction(PlayerContext context)
    {
        if (playSound)
        {
            if (soundSourcePlayer != null)
            {
                soundSourcePlayer.Play();
            }
        }
        Destroy(gameObject);
    }
}