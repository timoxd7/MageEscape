using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarrel : MonoBehaviour
{
    public Animator barrelAnimator;
    public string moveBarrelTrigger;
    public string playerTag;

    private bool playerInside;

    public void RequestMove()
    {
        if (!gameObject.activeSelf || !enabled)
            return;

        if (playerInside)
        {
            if (barrelAnimator != null)
            {
                barrelAnimator.SetTrigger(moveBarrelTrigger);
            } else
            {
                Debug.LogError("No Animator given!", this);
            }
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
