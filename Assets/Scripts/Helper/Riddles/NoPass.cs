using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPass : MonoBehaviour
{
    public string playerTag;
    [Tooltip("The Properties for the 'do not cross' message")]
    public DialogProperties dialogProperties;
    [Tooltip("The Message to be shown it the player tries to cross")]
    public DialogMessage stayBackMessage;
    [Tooltip("The Position (x/y) the Player should be reset after beeing cached")]
    public Transform playerResetPoint;

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (playerTag == "")
        {
            Debug.LogError("Player Tag not set!", this);
            return;
        }

        if (dialogProperties == null)
        {
            Debug.LogError("DialogProperties not set!", this);
            return;
        }

        if (stayBackMessage == null)
        {
            Debug.LogError("StayBackMessage not set!", this);
            return;
        }

        if (other.gameObject.tag == playerTag)
        {
            PlayerAccessibility playerAccessibility = other.gameObject.GetComponentInChildren<PlayerAccessibility>();

            if (playerAccessibility == null)
            {
                Debug.LogError("PlayerAccessibility could not be found on player!", this);
                return;
            }

            dialogProperties.player = playerAccessibility;
            player = other.gameObject;
            stayBackMessage.Show();
        }
    }

    public void resetPlayer()
    {
        CharacterController playerController = player.GetComponent<CharacterController>();
        playerController.enabled = false;
        player.transform.position = new Vector3(playerResetPoint.position.x, player.transform.position.y, playerResetPoint.position.z);
        playerController.enabled = true;
    }
}
