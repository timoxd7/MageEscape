using UnityEngine;

public class MagierFlyAnimation : MonoBehaviour
{
    public float circleTime = 2f;
    public float maxY = 2f;
    public float minY = 1f;
    public float zeroY = 0f;
    public float flyDownTime = 3f;

    private float currentTime = 0;
    private bool doingUnfly = false;
    private float yAtUnfily;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (!doingUnfly)
        {
            currentTime %= circleTime;

            float currentPos = ((Mathf.Sin((currentTime / circleTime) * 2f * Mathf.PI) + 1f) / 2f) * (maxY - minY) + minY;

            transform.localPosition = new Vector3(transform.localPosition.x, currentPos, transform.localPosition.z);
        } else
        {
            float currentPos = ((Mathf.Cos((currentTime / flyDownTime) * 1f * Mathf.PI) + 1f) / 2f) * (yAtUnfily - zeroY) + zeroY;
            bool destroyThis = false;

            if (currentTime >= flyDownTime)
            {
                currentPos = zeroY;
                destroyThis = true;
            }

            transform.localPosition = new Vector3(transform.localPosition.x, currentPos, transform.localPosition.z);

            if (destroyThis)
                Destroy(this);
        }
    }

    public void UnFly()
    {
        if (doingUnfly)
            return;

        yAtUnfily = transform.localPosition.y;
        doingUnfly = true;
        currentTime = 0f;
    }
}
