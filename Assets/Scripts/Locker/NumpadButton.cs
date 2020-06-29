using UnityEngine;

public class NumpadButton : BaseInteraction
{
    public NumpadController numpadController;
    public NumpadController.NumpadButtonType numpadButtonType;

    public override void OnInteraction(PlayerContext context)
    {
        if (numpadController != null)
        {
            numpadController.ButtonPressed(numpadButtonType);
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
