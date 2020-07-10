using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public RectTransform credits;
    public LevelLoader levelLoader;
    public string levelToLoadOnEnd;
    public float minYPosition;
    public float maxYPosition;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        credits.localPosition = new Vector3(credits.localPosition.x, minYPosition, credits.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        credits.localPosition = new Vector3(credits.localPosition.x, credits.localPosition.y + (speed * Time.deltaTime), credits.localPosition.z);

        if (credits.localPosition.y >= maxYPosition)
        {
            levelLoader.LoadLevel(levelToLoadOnEnd);
        }
    }
}
