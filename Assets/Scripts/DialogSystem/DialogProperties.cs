using MyBox;
using UnityEngine;

public class DialogProperties : MonoBehaviour
{
    [Header("Appearance")]
    public GameObject textPrefab;
    public Vector2 textPosition;
    public GameObject buttonPrefab;
    public Vector2 optionsOrigin;
    public float optionsOffset;

    [Header("Player")]
    public bool autoAssignPlayer = false;
    [ConditionalField(nameof(autoAssignPlayer), true)]
    public PlayerAccessibility player;
    [ConditionalField(nameof(autoAssignPlayer))]
    public string playerTag = "Player";

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

        

        if (autoAssignPlayer)
        {
            GameObject playerObject = GameObject.FindWithTag(playerTag);

            if (playerObject == null)
            {
                Debug.LogError("No player found in scene!", this);
            } else
            {
                player = playerObject.GetComponent<PlayerAccessibility>();
            }
        }
        
        if (player == null)
        {
            Debug.LogError("No player attached!", this); ;
            returnVal = false;
        }

        return returnVal;
    }
}
