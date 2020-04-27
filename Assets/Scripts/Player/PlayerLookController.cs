using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLookController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The Camera this Player is attached to")]
    public Transform usedCamera;
    [Tooltip("The Character (the camera should be a child of it)")]
    public Transform character;

    [Header("Settings")]
    public float lookSensitivity = 120f;
    public float maxUpPosition = -90;
    public float maxDownPosition = 90;

    private float xRotation = 0;
    private bool lastKnownFullscreedMode = false;
    private Vector2 lookSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Hide Mouse while in-game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock / Unlock Mouse Cursor (PC Keyboard/Mouse Spezific)

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Show Courser again
            Cursor.lockState = CursorLockMode.None;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Hide Courser again
            Cursor.lockState = CursorLockMode.Locked;
        }

        bool currentInFullscreen = Screen.fullScreen;
        if (currentInFullscreen != lastKnownFullscreedMode)
        {
            lastKnownFullscreedMode = currentInFullscreen;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
        }


        // Apply Look Speed

        float deltaX = lookSpeed.x * lookSensitivity * Time.deltaTime;
        float deltaY = lookSpeed.y * lookSensitivity * Time.deltaTime;

        xRotation += deltaX;
        xRotation = Mathf.Clamp(xRotation, Mathf.Min(maxUpPosition, maxDownPosition), Mathf.Max(maxUpPosition, maxDownPosition));

        usedCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        character.Rotate(new Vector3(0f, deltaY, 0f));
    }

    public void UpdateView(Vector2 deltaLook)
    {
        lookSpeed = deltaLook;
    }
}
