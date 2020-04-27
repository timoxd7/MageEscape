using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;
    public Transform character;
    public Transform groundCheck;
    public Camera usedCamera;

    [Header("Player Movement Settings")]
    public float movementSpeed = 5f;
    public float sprintMultiplyer = 2f;

    [Header("Grounding")]
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float groundDistance = 0.55f;
    public float forcedYVelocity = -2f;

    [Header("Jump")]
    public float jumpHeight = 1.0f;

    [Header("Sneak")]
    public float sneakingHeight = 1.4f;
    public float sneakingSpeedMultiplyer = 0.5f;
    public bool lockJumpAtSneaking = true;
    [MyBox.ConditionalField("lockJumpAtSneaking", true)]
    public float sneakingJumpHeightMultiplyer = 0.5f;
    public bool lockSprintAtSneaking = true;

    private Vector2 horizontalSpeed;
    private float yVelocity = 0f;
    private bool isGrounded;
    private bool sprinting = false;

    // Temp Values for Sneaking and returning to normal
    private float normalHeight;
    private float cameraHeadOffset;
    private bool sneaking = false;

    private void Start()
    {
        normalHeight = characterController.height;
        cameraHeadOffset = normalHeight - usedCamera.transform.localPosition.y;
    }

    void Update()
    {
        // Add horizontal movement speed
        Vector3 move = character.right * horizontalSpeed.x + character.forward * horizontalSpeed.y;
        move *= movementSpeed;

        if ((sprinting && !sneaking) || (sprinting && sneaking && !lockSprintAtSneaking))
        {
            move *= sprintMultiplyer;
        }

        if (sneaking)
        {
            move *= sneakingSpeedMultiplyer;
        }


        // Add gravity
        CheckGrounding();

        if (isGrounded && yVelocity < forcedYVelocity)
        {
            yVelocity = forcedYVelocity;
        }

        if (!isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
        }

        move.y = yVelocity;


        // Apply to character
        characterController.Move(move * Time.deltaTime);
    }

    public void SetSpeed(Vector2 newDirectionalSpeed)
    {
        horizontalSpeed = newDirectionalSpeed;
    }

    public void StartSprinting()
    {
        sprinting = true;
    }

    public void StopSprinting()
    {
        sprinting = false;
    }

    public void StartSneak()
    {
        sneaking = true;
        SetPlayerHeight(sneakingHeight);
    }

    public void StopSneak()
    {
        sneaking = false;
        SetPlayerHeight(normalHeight);
    }

    private void SetPlayerHeight(float newHeight)
    {
        // Set Capsule
        characterController.height = newHeight;
        characterController.center = new Vector3(characterController.center.x, newHeight / 2, characterController.center.z);

        // Set Camera
        usedCamera.transform.localPosition = new Vector3(usedCamera.transform.localPosition.x, newHeight - cameraHeadOffset, usedCamera.transform.localPosition.z);
    }

    public void Jump()
    {
        float currentJumpHeight = jumpHeight;

        if (sneaking)
        {
            if (lockJumpAtSneaking)
                return;
            else
                currentJumpHeight *= sneakingJumpHeightMultiplyer;
        }

        CheckGrounding();

        if (isGrounded)
        {
            yVelocity = Mathf.Sqrt(currentJumpHeight * -2f * gravity); // Because physics!
        }
    }

    private void CheckGrounding()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
