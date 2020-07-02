using MyBox;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 force;

    public void ApplyForce()
    {
        if (rb == null)
            rb = gameObject.GetOrAddComponent<Rigidbody>();

        rb.AddForce(force);
    }
}
