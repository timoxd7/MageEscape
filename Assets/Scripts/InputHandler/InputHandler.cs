using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public MovementInputData movementInputData;
    public InteractionInputData interactionInputData;


    private void Start()
    {
        movementInputData.ResetInput();
        interactionInputData.ResetInput();
    }

    void Update()
    {
        GetMovementInput();
        GetInteractionInput();
    }

    private void GetInteractionInput()
    {
        interactionInputData.InteractClicked = Input.GetKeyDown(KeyCode.E);
        interactionInputData.InteractReleased = Input.GetKeyUp(KeyCode.E);
    }

    private void GetMovementInput()
    {
        movementInputData.Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
