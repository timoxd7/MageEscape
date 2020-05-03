using MyBox;
using UnityEngine;

public class ShootUp : BaseInteraction
{
    public float shootForce = 1000f;

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetOrAddComponent<Rigidbody>();
    }
    public override void OnInteraction()
    {
        rb.AddForce(new Vector3(0f, shootForce, 0f));
    }
}
