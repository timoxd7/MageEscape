using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The Camera this Player is attached to")]
    public Transform usedCamera;
    [Tooltip("The Character (the camera should be a child of it)")]
    public Transform character;

    [Header("Settings")]
    public float lookSensitivity = 0.1f;
    public float maxUpPosition = -90;
    public float maxDownPosition = 90;

    private float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Hide Mouse while in-game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock / Unlock Mouse Cursor

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
    }

    public void UpdateView(Vector2 deltaLook)
    {
        float deltaX = deltaLook.x * lookSensitivity;
        float deltaY = deltaLook.y * lookSensitivity;

        xRotation += deltaX;
        xRotation = Mathf.Clamp(xRotation, Mathf.Min(maxUpPosition, maxDownPosition), Mathf.Max(maxUpPosition, maxDownPosition));

        usedCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        character.Rotate(new Vector3(0f, deltaY, 0f));
    }
}
