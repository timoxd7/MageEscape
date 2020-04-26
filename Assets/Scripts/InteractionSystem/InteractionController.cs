using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Data")]
    public InteractionInputData interactionInputData;
    public InteractionData interactionData;

    [Space]
    [Header("Ray Settings")]
    public float rayDistance;
    public float raySphereRadius;
    public LayerMask interactableLayer;

    private Camera cam;

    // Builtin Methods

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
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

    }
}
