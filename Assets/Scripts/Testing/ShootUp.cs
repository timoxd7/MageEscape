using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class ShootUp : BaseInteractable
{
    public float shootForce = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetOrAddComponent<Rigidbody>();
    }

    public override void OnInteract()
    {
        rb.AddForce(new Vector3(0f, shootForce, 0f));
    }
}
