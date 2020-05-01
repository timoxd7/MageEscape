using UnityEngine;

public class RotateInteraction : BaseInteraction
{
    public override void OnInteraction()
    {
        gameObject.transform.Rotate(20, 20, 20);
        
    }
}
