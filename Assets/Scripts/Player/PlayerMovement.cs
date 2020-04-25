using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public float sprintSpeed = 10f;
    private float translation;
    private float straffe;

    // Use this for initialization
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float speedModifier = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedModifier = sprintSpeed;
        }
        else
        {
            speedModifier = speed;
        }
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speedModifier * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speedModifier * Time.deltaTime;
        transform.Translate(straffe, 0, translation);
    }
}