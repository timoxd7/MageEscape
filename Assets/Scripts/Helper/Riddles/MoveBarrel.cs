using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarrel : BaseInteraction
{
    public Animator barrelAnimator;
    public string moveBarrelTrigger;
    public string playerTag;
    public HighlightDetection highlightToDisable;
    public Interactable interactableToDisable;

    private bool playerInside;

    public override void OnInteraction(PlayerContext context)
    {
        if (playerInside)
        {
            if (barrelAnimator != null)
            {
                barrelAnimator.SetTrigger(moveBarrelTrigger);

                if (highlightToDisable != null)
                {
                    highlightToDisable.enabled = false;
                    Destroy(highlightToDisable);
                } else
                {
                    Debug.LogError("No Highlight to Disable given!", this);
                }

                if (interactableToDisable != null)
                {
                    interactableToDisable.enabled = false;
                    Destroy(interactableToDisable);
                }
                else
                {
                    Debug.LogError("No interactableToDisable given!", this);
                }
            } else
            {
                Debug.LogError("No Animator given!", this);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            playerInside = false;
        }
    }
}
