using MyBox;
using UnityEngine;

public class ShootUp : BaseInteraction
{
    public GameObject objectToShoot;
    public float shootForce = 1000f;

    private Rigidbody rb;

    private void Start()
    {
        if (objectToShoot == null)
            objectToShoot = gameObject;

        rb = objectToShoot.GetOrAddComponent<Rigidbody>();
    }

    public override void OnInteraction(PlayerContext context)
    {
        Shoot();
    }

    [ButtonMethod]
    public void Shoot()
    {
        rb.AddForce(new Vector3(0f, shootForce, 0f));
    }
}
