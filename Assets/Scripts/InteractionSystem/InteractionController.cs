using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("References")]
    public Camera cam;

    [Header("Data")]
    public InteractionData interactionData;

    [Space]
    [Header("Ray Settings")]
    public float rayDistance;
    public float raySphereRadius;
    public LayerMask interactableLayer;


    // Private
    private bool interactingNow;
    private Timer interactingTimer;

    private bool interactionPush = false;
    private bool interactionRelease = false;

    // Builtin Methods

    private void Awake()
    {
        interactingTimer = new Timer();
    }

    private void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }

    public void InteractPush()
    {
        interactionPush = true;
        Update();
    }

    public void InteractRelease()
    {
        interactionRelease = true;
        Update();
    }

    // Custom Methods

    void CheckForInteractable()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        RaycastHit hitInfo;
        bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out hitInfo, rayDistance, interactableLayer);

        if (hitSomething)
        {
            // Wenn wir etwas treffen schauen wir ob das Objekt eine Interaktionskomponente BaseInteractable hat
            BaseInteractable interactable = hitInfo.transform.GetComponent<BaseInteractable>();
            if (interactable != null)
            {
                if (interactionData.IsEmpty())
                {
                    // Wenn jetzt unser aktueller Slot für Interaktion leer ist, dann können wir das gefundene Objekt dem Slot zuweisen
                    interactionData.Interactable = interactable;
                }
                else
                {
                    // Wenn der Slot nicht leer ist wollen wir wissen ob das gefundene Objekt nicht ohnehin schon unser aktuelles ist
                    if (!interactionData.IsSameInteractable(interactable))
                    {
                        // Wenn nicht, dann jetzt schon
                        interactionData.Interactable = interactable;
                    }
                }
            }
        }
        else
        {
            // Unser Ray hat nichts getroffen, es gitb kein Objekt zum interagieren
            interactionData.ResetData();
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
    }

    void CheckForInteractableInput()
    {
        if (interactionData.IsEmpty())
        {
            return;
        }

        if (interactionPush)
        {
            interactionPush = false;
            interactingNow = true;
            interactingTimer.Restart();
        }

        if (interactionRelease)
        {
            interactionRelease = false;
            interactingNow = false;
            interactingTimer.Reset();
        }

        if (interactingNow)
        {
            if (!interactionData.Interactable.IsInteractable)
            {
                return;
            }

            if (interactionData.Interactable.HoldToInteract)
            {
                if (interactingTimer.Get() > interactionData.Interactable.HoldDuration)
                {
                    interactionData.Interact();
                    interactingNow = false;
                }
            }
            else
            {
                interactionData.Interact();
                interactingNow = false;
            }
        }
    }
}
