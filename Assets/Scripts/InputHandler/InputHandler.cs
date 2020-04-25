using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 movementInput { get; private set;}

    void Update()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
