using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLookController : MonoBehaviour
{
    #region Public Vars

    [Header("References")]
    [Tooltip("The Camera this Player is attached to")]
    public Transform usedCamera;
    [Tooltip("The Character (the camera should be a child of it)")]
    public Transform character;

    [Header("Sensitivity")]
    public float velocityModeSensitivity = 120f;
    public float motionModeSensitivity = 12f;

    [Header("Settings")]
    public float maxUpPosition = -90;
    public float maxDownPosition = 90;

    [Header("Debug")]
    public DebugGUIWriter debugGuiWriter;
    public bool guiShowCurrentLookMode = false;

    #endregion

    #region Private Vars

    private float xRotation = 0;
    private bool lastKnownFullscreedMode = false;
    private Vector2 deltaLook;
    private LookMode currentLookMode;

    #endregion

    #region Builtin

    void Start()
    {
        // Hide Mouse while in-game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Lock / Unlock Mouse Cursor (PC Keyboard/Mouse Spezific)

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Show Courser again
            Cursor.lockState = CursorLockMode.None; // !!! Remove later for the Use of a menu
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

        if (currentLookMode == LookMode.Velocity)
        {
            // Apply current Speed to the Rotation of the Camera to its time-based current rotation
            ApplyRotation(deltaLook * velocityModeSensitivity * Time.deltaTime);
        } else if (currentLookMode == LookMode.Motion)
        {
            // Add all incremental Rotations since the last Frame
            ApplyRotation(deltaLook * motionModeSensitivity);
            // Reset all "consumed" rotation
            deltaLook.Set(0, 0);
        }


        // Add Debug Info
        if (guiShowCurrentLookMode && debugGuiWriter != null)
        {
            string currentLookModeString;

            if (currentLookMode == LookMode.Velocity)
                currentLookModeString = "Velocity";
            else if (currentLookMode == LookMode.Motion)
                currentLookModeString = "Motion";
            else
                currentLookModeString = "Unknown";

            debugGuiWriter.AddLine("Look Mode: " + currentLookModeString);
        }
    }

    #endregion

    #region Update Look

    public void UpdateViewVelocity(Vector2 deltaLook)
    {
        // Set current Velocity of the Look-Motion
        currentLookMode = LookMode.Velocity;
        this.deltaLook = deltaLook;
    }

    public void UpdateViewMotion(Vector2 deltaLook)
    {
        // Add the new incremental Look-Movement
        currentLookMode = LookMode.Motion;
        this.deltaLook += deltaLook;
    }

    private void ApplyRotation(Vector2 deltaRotation)
    {
        xRotation += deltaRotation.x;
        xRotation = Mathf.Clamp(xRotation, Mathf.Min(maxUpPosition, maxDownPosition), Mathf.Max(maxUpPosition, maxDownPosition));

        usedCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        character.Rotate(new Vector3(0f, deltaRotation.y, 0f));
    }

    #endregion

    #region Enums

    enum LookMode
    {
        Velocity,
        Motion
    }

    #endregion
}
