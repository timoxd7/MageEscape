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
    private readonly InteractionData _interactionData = new InteractionData();
    #endregion
    
    #region Builtin
    private void Update()
    {
        UpdateInteractionData();
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
    private void UpdateInteractionData()
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
                if (_interactionData.IsEmpty())
                {
                    // Wenn jetzt unser aktueller slot für detectables leer ist, können wir das gefundene Objekt dem Slot zuweisen
                    _interactionData.CurrentInteractable = detected;
                }
                else
                {
                    // Wenn der Slot nicht leer ist wollen wir wissen ob das gefundene Objekt nicht ohnehin schon unser aktuelles ist
                    if (!_interactionData.IsSame(detected))
                    {
                        // Wenn nicht, dann jetzt schon
                        _interactionData.CurrentInteractable = detected;
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
            _interactionData.Reset();
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

        // Case 1
        if (_interactionData.LastInteractable == null && !_interactionData.IsEmpty())
        {
            _interactionData.CurrentInteractable.OnDetectionEnter();
            _interactionData.LastInteractable = _interactionData.CurrentInteractable;
            return;
        }

        // Case 2
        if (_interactionData.LastInteractable != null && _interactionData.CurrentInteractable != null)
        {
            if (!_interactionData.IsSame(_interactionData.LastInteractable))
            {
                _interactionData.LastInteractable.OnDetectionExit();
                _interactionData.CurrentInteractable.OnDetectionEnter();
                _interactionData.LastInteractable = _interactionData.CurrentInteractable;
                return;
            }
        }

        // Case 3
        if (_interactionData.LastInteractable != null && _interactionData.IsEmpty())
        {
            _interactionData.LastInteractable.OnDetectionExit();
            _interactionData.LastInteractable = null;
            return;
        }
    }
    
    private void CheckInput()
    {
        if(_interactionData.IsEmpty())
        {
            return;
        }

        Interactable interactable = _interactionData.CurrentInteractable;

        if (_interactionInput.InteractPush)
        {
            _interactionInput.InteractPush = false;
            _interactionData.InteractingNow = true;
            _interactionTimer.Restart();
        }

        if (_interactionInput.InteractRelease)
        {
            _interactionInput.InteractRelease = false;
            _interactionData.InteractingNow = false;
            _interactionTimer.Reset();
            
        }

        if (_interactionData.InteractingNow)
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
                    interactable.OnInteraction();
                    _interactionData.InteractingNow = false;
                }
            }
            else
            {
                _interactionTimer.Reset();
                interactable.OnInteraction();
                _interactionData.InteractingNow = false;
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

    private class InteractionData
    {
        public Interactable LastInteractable { get; set; }
        public Interactable CurrentInteractable { get; set; }
        
        public bool InteractingNow { get; set; }
    
        public bool IsSame(Interactable detectable)
        {
            return CurrentInteractable == detectable;
        }
    
        public void Reset()
        {
            LastInteractable = CurrentInteractable;
            CurrentInteractable = null;
            InteractingNow = false;
        }
    
        public bool IsEmpty()
        {
            return CurrentInteractable == null;
        }
    }
}
