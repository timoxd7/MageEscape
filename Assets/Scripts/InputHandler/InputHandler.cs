using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{   
    [Header("References")]
    public MouseLook mouseLook;
    public PlayerMovement playerMovement;
    public InteractionController interactionController;
    
    private InputMaster inputMaster;


    public void Awake()
    {
        inputMaster = new InputMaster();

        // Interaction
        inputMaster.Player.Interact.performed += _ => interactionController.InteractPush();
        inputMaster.Player.Interact.canceled += _ => interactionController.InteractRelease();

        // Player Controller
        inputMaster.Player.Move.performed += context => playerMovement.SetSpeed(context.ReadValue<Vector2>());
        inputMaster.Player.Sprint.performed += _ => playerMovement.StartSprinting();
        inputMaster.Player.Sprint.canceled += _ => playerMovement.StopSprinting();
        inputMaster.Player.Jump.performed += _ => playerMovement.Jump();
        inputMaster.Player.Look.performed += context => mouseLook.UpdateView(context.ReadValue<Vector2>());
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
