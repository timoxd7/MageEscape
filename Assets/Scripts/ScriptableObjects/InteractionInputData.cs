using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InputData/MovementInput")]
public class InteractionInputData : ScriptableObject
{
    private bool interactClicked;
    private bool interactReleased;

    public bool InteractClicked
    {
        get => interactClicked;
        set => interactClicked = value;
    }

    public bool InteractReleased
    {
        get => interactReleased;
        set => interactReleased = value;
    }

    public void ResetInput()
    {
        InteractClicked = false;
        InteractReleased = false;
    }
}
