using MyBox;
using System.Collections.Generic;
using UnityEngine;

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
    [Tooltip("The vertical speed the player will get in sneaking position")]
    public float sneakMovementSpeed = 4.0f;
    [Tooltip("The multiplyer which will be applied on the horizontal movement speed of the player")]
    public float sneakingSpeedMultiplyer = 0.5f;
    public bool lockJumpAtSneaking = true;
    [MyBox.ConditionalField("lockJumpAtSneaking", true)]
    public float sneakingJumpHeightMultiplyer = 0.5f;
    public bool lockSprintAtSneaking = true;

    [Header("Settings for spezific areas")]
    public bool jumpEnabled = true;
    public bool sneakEnabled = true;
    public bool sprintEnabled = true;

    [Header("Sound")]
    public float stepSoundDistance = 1f;
    public float stepSoundSneakMultiplyer = 1f;
    public float stepSoundRunMultiplyer = 1f;
    public List<SoundSourcePlayer> stepSounds;
    public List<SoundSourcePlayer> jumpSounds;


    private Vector2 horizontalSpeed;
    private float yVelocity = 0f;
    private bool isGrounded;
    private bool sprinting = false;

    // Temp Values for Sneaking and returning to normal
    private float normalHeight;
    private float cameraHeadOffset;
    private bool sneaking = false;
    private float currentHeight;

    // Incremental Move Distance since last sound
    private float unusedMoveDistance = 0f;

    private void Start()
    {
        normalHeight = characterController.height;
        currentHeight = normalHeight;
        cameraHeadOffset = normalHeight - usedCamera.transform.localPosition.y;
    }

    void Update()
    {
        // Add horizontal movement speed
        Vector3 move = character.right * horizontalSpeed.x + character.forward * horizontalSpeed.y;
        Vector2 horizontalMove = new Vector2(move.x, move.z);
        float moveAbsolute = horizontalMove.magnitude;
        move *= movementSpeed;

        if (sprintEnabled)
        {
            if ((sprinting && !sneaking) || (sprinting && sneaking && !lockSprintAtSneaking))
            {
                move *= sprintMultiplyer;
                moveAbsolute *= sprintMultiplyer * stepSoundRunMultiplyer;
            }

        }

        if (sneaking && sneakEnabled)
        {
            move *= sneakingSpeedMultiplyer;
            moveAbsolute *= sneakingSpeedMultiplyer * stepSoundSneakMultiplyer;
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


        // Apply movement to character
        characterController.Move(move * Time.deltaTime);

        // Apply Sound
        unusedMoveDistance += moveAbsolute;

        if (unusedMoveDistance >= stepSoundDistance)
        {
            unusedMoveDistance -= stepSoundDistance;
            PlayStepSound();
        }


        // Apply Sneeking if needed
        if (sneaking && (currentHeight != sneakingHeight) && sneakEnabled)
        {
            currentHeight -= sneakMovementSpeed * Time.deltaTime;
            if (currentHeight < sneakingHeight)
                currentHeight = sneakingHeight;
            UpdatePlayerHeight();
        }
        else if ((!sneaking && currentHeight != normalHeight) || (currentHeight != normalHeight && !sneakEnabled))
        {
            currentHeight += sneakMovementSpeed * Time.deltaTime;
            if (currentHeight > normalHeight)
                currentHeight = normalHeight;
            UpdatePlayerHeight();
        }
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
    }

    public void StopSneak()
    {
        sneaking = false;
    }

    private void UpdatePlayerHeight()
    {
        // Set Capsule
        characterController.height = currentHeight;
        characterController.center = new Vector3(characterController.center.x, currentHeight / 2, characterController.center.z);

        // Set Camera
        usedCamera.transform.localPosition = new Vector3(usedCamera.transform.localPosition.x, currentHeight - cameraHeadOffset, usedCamera.transform.localPosition.z);
    }

    public void Jump()
    {
        if (!enabled)
            return;

        if (jumpEnabled)
        {
            float currentJumpHeight = jumpHeight;

            if (sneaking && sneakEnabled)
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
                PlayJumpSound();
            }
        }
    }

    private void CheckGrounding()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void PlayStepSound()
    {
        if (!stepSounds.IsNullOrEmpty())
        {
            PlayRandom(stepSounds);
        } else
        {
            Debug.LogError("No StepSounds given!", this);
        }
    }

    private void PlayJumpSound()
    {
        if (!jumpSounds.IsNullOrEmpty())
        {
            PlayRandom(jumpSounds);
        } else
        {
            Debug.LogError("No JumpSounds given!");
        }
    }

    private void PlayRandom(List<SoundSourcePlayer> soundSourcePlayer)
    {
        if (soundSourcePlayer.Count == 0)
            return;

        int randomIndex = Random.Range(0, soundSourcePlayer.Count);

        if (randomIndex > soundSourcePlayer.Count - 1)
            randomIndex = soundSourcePlayer.Count - 1;
        else if (randomIndex < 0)
            randomIndex = 0;

        soundSourcePlayer[randomIndex].Play();
    }
}
