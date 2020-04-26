using UnityEngine;

[CreateAssetMenu(fileName = "MovementInputData", menuName = "InputData/MovementInput")]
public class MovementInputData : ScriptableObject
{
    private Vector2 movement;

    public Vector2 Movement
    {
        get => movement;
        set => movement = value;
    }

    public void ResetInput()
    {
        movement = new Vector2(0f, 0f);
    }
}