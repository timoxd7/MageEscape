using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InputData/MovementInput")]
public class InteractionInputData : ScriptableObject
{
    public bool InteractClicked { get; set; }
    public bool InteractReleased { get; set; }

    public void ResetInput()
    {
        InteractClicked = false;
        InteractReleased = false;
    }
}
