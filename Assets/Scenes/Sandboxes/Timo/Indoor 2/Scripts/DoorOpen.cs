using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator animator;
    public string doorOpenAnimationTrigger;
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            animator.SetTrigger(doorOpenAnimationTrigger);
        }
    }
}
