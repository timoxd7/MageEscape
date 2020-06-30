using MyBox;
using UnityEngine;

public class NumpadButton : BaseInteraction
{
    public NumpadController numpadController;
    public NumpadController.NumpadButtonType numpadButtonType;

    public bool playSound = false;
    [ConditionalField(nameof(playSound))]
    public SoundSourcePlayer soundSourcePlayer;

    public override void OnInteraction(PlayerContext context)
    {
        if (numpadController != null)
        {
            numpadController.ButtonPressed(numpadButtonType);

            if (playSound && soundSourcePlayer != null)
                soundSourcePlayer.Play();
        } else
        {
            Debug.LogError("No NumpadController given!", this);
        }
    }

    public void Deactivate()
    {
        Interactable thisInteractable = GetComponent<Interactable>();
        HighlightDetection thisHighlight = GetComponent<HighlightDetection>();

        if (thisInteractable == null)
        {
            Debug.LogError("No Interactable attached!", this);
        } else
        {
            Destroy(thisInteractable);
        }

        if (thisHighlight == null)
        {
            Debug.LogError("No HighlightDetection attached!", this);
        } else
        {
            thisHighlight.enabled = false;
            Destroy(thisHighlight);
        }

        Destroy(this);
    }
}
