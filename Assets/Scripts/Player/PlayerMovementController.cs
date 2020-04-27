using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;
    public Transform character;
    public Transform groundCheck;

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

    private Vector2 horizontalSpeed;
    private float yVelocity = 0f;
    private bool isGrounded;
    private bool sprinting = false;


    void Update()
    {
        // Add horizontal movement speed
        Vector3 move = character.right * horizontalSpeed.x + character.forward * horizontalSpeed.y;
        move *= movementSpeed;

        if (sprinting)
        {
            move *= sprintMultiplyer;
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

    public void Jump()
    {
        CheckGrounding();

        if (isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); // Because physics!
        }
    }

    private void CheckGrounding()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
