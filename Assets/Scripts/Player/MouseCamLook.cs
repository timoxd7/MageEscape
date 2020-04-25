using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    public float sensitivity = 120.0f;
    public float smoothing = 2.0f;
    // the chacter is the capsule
    public GameObject character;
    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;

    public float verticalMax = 50f;

    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;

        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // md is mouse delta
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        // the interpolated float result between the two float values
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing) * Time.fixedDeltaTime;
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing) * Time.fixedDeltaTime;

        // incrementally add to the camera look
        mouseLook += smoothV;

        if (Mathf.Abs(mouseLook.y) > verticalMax)
        {
            mouseLook.y = Mathf.Sign(mouseLook.y) * verticalMax;
        }

        // vector3.right means the x-axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);


        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
}