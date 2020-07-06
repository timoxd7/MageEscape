using MyBox;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class DisableInteractableOnTrigger : MonoBehaviour
{
    public List<GameObject> interactables;

    private void OnTriggerEnter(Collider other)
    {
        if (interactables.IsNullOrEmpty())
            return;

        foreach (GameObject currentInteractable in interactables)
        {
            if (currentInteractable != null)
            {
                if (other.gameObject == currentInteractable)
                {
                    Interactable interactable = currentInteractable.GetComponent<Interactable>();

                    if (interactable != null)
                        interactable.enabled = false;
                    else
                    {
                        interactable = currentInteractable.GetComponentInChildren<Interactable>();

                        if (interactable != null)
                            interactable.enabled = false;
                        else
                            Debug.LogError("No interactable on object!", this);
                    }
                }
            }
        }
    }
}
