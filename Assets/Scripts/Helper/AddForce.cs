using MyBox;
using UnityEngine;

public class AddForce : BaseInteraction
{
    public Rigidbody rb;
    public Vector3 force;

    public void ApplyForce()
    {
        if (rb == null)
            rb = gameObject.GetOrAddComponent<Rigidbody>();

        rb.AddForce(force);
    }

    public override void OnInteraction(PlayerContext context)
    {
        ApplyForce();
    }
}
