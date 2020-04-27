using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{   
    [Header("References")]
    public PlayerLookController playerLookController;
    public PlayerMovementController playerMovementController;
    public InteractionController interactionController;
    
    private InputMaster inputMaster;


    public void Awake()
    {
        inputMaster = new InputMaster();

        // Interaction
        inputMaster.Player.Interact.performed += _ => interactionController.InteractPush();
        inputMaster.Player.Interact.canceled += _ => interactionController.InteractRelease();

        // Player Controller
        inputMaster.Player.Move.performed += context => playerMovementController.SetSpeed(context.ReadValue<Vector2>());
        inputMaster.Player.Sprint.performed += _ => playerMovementController.StartSprinting();
        inputMaster.Player.Sprint.canceled += _ => playerMovementController.StopSprinting();
        inputMaster.Player.Jump.performed += _ => playerMovementController.Jump();
        inputMaster.Player.Look.performed += context => playerLookController.UpdateView(context.ReadValue<Vector2>());
        inputMaster.Player.Sneak.performed += _ => playerMovementController.StartSneak();
        inputMaster.Player.Sneak.canceled += _ => playerMovementController.StopSneak();
    }


    // -------------------------------- Others --------------------------------


    public void OnEnable()
    {
        inputMaster.Enable();
    }

    public void OnDisable()
    {
        inputMaster.Disable();
    }

}
