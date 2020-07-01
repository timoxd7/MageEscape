using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 force;

    public void ApplyForce()
    {
        rb.AddForce(force);
    }
}
