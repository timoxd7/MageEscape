using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogProperties : MonoBehaviour
{
    public GameObject textPrefab;
    public Vector2 textPosition;
    public GameObject buttonPrefab;
    public Vector2 optionsOrigin;
    public float optionsOffset;
    public PlayerAccessibility player;

    public bool Validate()
    {
        bool returnVal = true;

        if (textPrefab == null)
        {
            Debug.LogError("No textPrefab attached!", this); ;
            returnVal = false;
        }

        if (buttonPrefab == null)
        {
            Debug.LogError("No buttonPrefab attached!", this); ;
            returnVal = false;
        }

        if (player == null)
        {
            Debug.LogError("No player attached!", this); ;
            returnVal = false;
        }

        return returnVal;
    }
}
