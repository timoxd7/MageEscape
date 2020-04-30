using UnityEngine;

public class RotateInteraction : MonoBehaviour, IInteractable
{
    public void OnInteraction()
    {
        gameObject.transform.Rotate(20, 20, 20);
        
    }
}
