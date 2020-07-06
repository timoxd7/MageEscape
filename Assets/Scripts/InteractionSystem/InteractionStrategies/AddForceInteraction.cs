using MyBox;
using UnityEngine;

public class AddForceInteraction : BaseInteraction
{
    public Rigidbody rb;
    public Vector3 force;

    public override void OnInteraction(PlayerContext playerContext)
    {
        if (rb == null)
            rb = gameObject.GetOrAddComponent<Rigidbody>();

        rb.AddForce(force);
    }
}
