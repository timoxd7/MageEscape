using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InputData")]
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

    public void Reset()
    {
        interactClicked = false;
        interactReleased = false;
    }
}
