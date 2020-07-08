using MyBox;
using UnityEngine;

public class PlayerAccessibility : MonoBehaviour
{
    public PlayerLookController playerLookController;
    public PlayerMovementController playerMovementController;
    public InteractionController interactionController;


    private bool lastKnownFullscreedMode = false;
    private bool playerLocked = false;


    void Start()
    {
        // Hide Mouse while in-game at start
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        bool currentInFullscreen = Screen.fullScreen;
        if (currentInFullscreen != lastKnownFullscreedMode && !playerLocked)
        {
            lastKnownFullscreedMode = currentInFullscreen;
            CaptureMouse();
        }
    }

    [ButtonMethod]
    public void LockPlayer()
    {
        LockPlayer(true);
    }
    
    public void LockPlayer(bool enableMouse)
    {
        if (CheckReferences())
        {
            playerLookController.enabled = false;
            playerMovementController.enabled = false;
            interactionController.enabled = false;

            playerLocked = true;
        }

        if (enableMouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    [ButtonMethod]
    public void ReleasePlayer()
    {
        if (CheckReferences())
        {
            playerLookController.enabled = true;
            playerMovementController.enabled = true;
            interactionController.enabled = true;

            playerLocked = false;
        }

        CaptureMouse();
    }

    public bool GetPlayerLocked()
    {
        return playerLocked;
    }

    [ButtonMethod]
    public void CaptureMouse()
    {
        Cursor.lockState = CursorLockMode.None; // Because its buggy...
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool CheckReferences()
    {
        bool returnVal = true;

        if (playerLookController == null)
        {
            Debug.LogError("No PlayerLookController given!", this);
            returnVal = false;
        }

        if (playerMovementController == null)
        {
            Debug.LogError("No PlayerMovementController given!", this);
            returnVal = false;
        }

        if (interactionController == null)
        {
            Debug.LogError("No InteractionController given!", this);
            returnVal = false;
        }

        return returnVal;
    }
}
