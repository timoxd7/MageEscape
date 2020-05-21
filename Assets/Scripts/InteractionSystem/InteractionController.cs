using UnityEngine;

public class InteractionController : MonoBehaviour
{
    #region Vars
    /*Settings*/
    [Header("Ray Settings")] 
    public Camera rayCam;
    public float rayDistance;
    public float raySpehereRadius;
    public LayerMask rayDetectableLayer;
    
    /*Utils*/
    private readonly Timer _interactionTimer = new Timer();
    private readonly InteractionInput _interactionInput = new InteractionInput();
    private readonly PlayerContext _playerContext = new PlayerContext();
    #endregion
    
    #region Builtin
    private void Update()
    {
        UpdateInteractionContext();
        CheckDetection();
        CheckInput();
    }
    #endregion

    #region Input handling

    public void InteractPush()
    {
        _interactionInput.InteractPush = true;
    }
    
    public void InteractRelease()
    {
        _interactionInput.InteractRelease = true;
    }

    #endregion

    #region Cutsom Methods
    private void UpdateInteractionContext()
    {
        var camTransform = rayCam.transform;
        Ray ray = new Ray(camTransform.position, camTransform.forward);

        bool hitSomething = Physics.SphereCast(ray, raySpehereRadius, out var hitInfo, rayDistance, rayDetectableLayer);

        if (hitSomething)
        {
            // Wenn wir etwas detected haben schauen wir ob das Objecte eine enrprechende Detactable Komponente hat
            Interactable detected = hitInfo.collider.GetComponent<Interactable>();
            if (detected != null)
            {
                if (_playerContext.InteractionData.IsEmpty())
                {
                    // Wenn jetzt unser aktueller slot für detectables leer ist, können wir das gefundene Objekt dem Slot zuweisen
                    _playerContext.InteractionData.CurrentInteractable = detected;
                }
                else
                {
                    // Wenn der Slot nicht leer ist wollen wir wissen ob das gefundene Objekt nicht ohnehin schon unser aktuelles ist
                    if (!_playerContext.InteractionData.IsSame(detected))
                    {
                        // Wenn nicht, dann jetzt schon
                        _playerContext.InteractionData.CurrentInteractable = detected;
                    }
                }
            } else
            {
                Debug.Log("Ray hit Object on interaction Layer but it has no Interactable!", gameObject);
            }
        }
        else
        {
            // Unser Ray hat nichts getroffen, es gitb kein Objekt zum interagieren
            _playerContext.InteractionData.Reset();
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
    }

    private void CheckDetection()
    {
        /*
         * 3 Transitions/Cases here:
         * 1. No Object -> New Object
         * 2. Current Object -> New Object
         * 3. Current Object -> No Object
         */

        InteractionData interactionData = _playerContext.InteractionData;

        // Case 1
        if (interactionData.LastInteractable == null && !interactionData.IsEmpty())
        {
            interactionData.CurrentInteractable.OnDetectionEnter();
            interactionData.LastInteractable = interactionData.CurrentInteractable;
            return;
        }

        // Case 2
        if (interactionData.LastInteractable != null && interactionData.CurrentInteractable != null)
        {
            if (!interactionData.IsSame(interactionData.LastInteractable))
            {
                interactionData.LastInteractable.OnDetectionExit();
                interactionData.CurrentInteractable.OnDetectionEnter();
                interactionData.LastInteractable = interactionData.CurrentInteractable;
                return;
            }
        }

        // Case 3
        if (interactionData.LastInteractable != null && interactionData.IsEmpty())
        {
            interactionData.LastInteractable.OnDetectionExit();
            interactionData.LastInteractable = null;
            return;
        }
    }
    
    private void CheckInput()
    {
        InteractionData interactionData = _playerContext.InteractionData;
        Interactable interactable = interactionData.CurrentInteractable;
        
        if(interactionData.IsEmpty())
        {
            return;
        }
        
        if (_interactionInput.InteractPush)
        {
            _interactionInput.InteractPush = false;
            interactionData.InteractingNow = true;
            _interactionTimer.Restart();
        }

        if (_interactionInput.InteractRelease)
        {
            _interactionInput.InteractRelease = false;
            interactionData.InteractingNow = false;
            _interactionTimer.Reset();
            
        }

        if (interactionData.InteractingNow)
        {
            if (!interactable.IsInteractable)
            {
                return;
            }

            if (interactable.HoldToInteract)
            {
                if (_interactionTimer.Get() > interactable.HoldDuration)
                {
                    _interactionTimer.Reset();
                    interactable.OnInteraction(_playerContext);
                    interactionData.InteractingNow = false;
                }
            }
            else
            {
                _interactionTimer.Reset();
                interactable.OnInteraction(_playerContext);
                interactionData.InteractingNow = false;
            }
        }
    }
    #endregion

    private class InteractionInput
    {
        public bool InteractPush { get; set; }

        public bool InteractRelease { get; set; }

        public void Reset()
        {
            InteractPush = false;
            InteractRelease = false;
        }
    }
}
