using UnityEngine;

public class InputHandler : MonoBehaviour
{   
    [Header("References")]
    public PlayerLookController playerLookController;
    public PlayerMovementController playerMovementController;
    public InteractionController interactionController;
    
    
    private InputMaster _inputMaster;


    public void Awake()
    {
        _inputMaster = new InputMaster();

        // Interaction
        if (interactionController != null)
        {
            _inputMaster.Player.Interact.performed += _ => interactionController.InteractPush();
            _inputMaster.Player.Interact.canceled += _ => interactionController.InteractRelease();
        }
        else
        {
            Debug.LogWarning("No InteractionController given");
        }

        // Player Movement
        if (playerMovementController != null)
        {
            _inputMaster.Player.Move.performed += context => playerMovementController.SetSpeed(context.ReadValue<Vector2>());
            _inputMaster.Player.Sprint.performed += _ => playerMovementController.StartSprinting();
            _inputMaster.Player.Sprint.canceled += _ => playerMovementController.StopSprinting();
            _inputMaster.Player.Jump.performed += _ => playerMovementController.Jump();
            _inputMaster.Player.Sneak.performed += _ => playerMovementController.StartSneak();
            _inputMaster.Player.Sneak.canceled += _ => playerMovementController.StopSneak();
        }
        else
        {
            Debug.LogWarning("No MovementController given");
        }

        // Player Looking
        if (playerLookController != null)
        {
            _inputMaster.Player.LookVelocityBased.performed += context => playerLookController.UpdateViewVelocity(context.ReadValue<Vector2>());
            _inputMaster.Player.LookMotionBased.performed += context => playerLookController.UpdateViewMotion(context.ReadValue<Vector2>());
        }
        else
        {
            Debug.LogWarning("No LookController given");
        }

    }


    // -------------------------------- Others --------------------------------


    public void OnEnable()
    {
        _inputMaster.Enable();
    }

    public void OnDisable()
    {
        _inputMaster.Disable();
    }

}
