using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InteractionInputData")]
public class InteractionInputData : ScriptableObject
{
    private bool interactClicked;
    private bool interactReleased;

    /**
     * @brief Das Clicked Event für InteractionInput
     * @return bool
     */
    public bool InteractClicked
    {
        get => interactClicked;
        set => interactClicked = value;
    }

    /**
     * @brief Das Released Event für InteractionInput
     * @return bool
     */
    public bool InteractReleased
    {
        get => interactReleased;
        set => interactReleased = value;
    }

    /**
     * @brief Den Input zurücksetzen
     */
    public void ResetInput()
    {
        InteractClicked = false;
        InteractReleased = false;
    }
}
