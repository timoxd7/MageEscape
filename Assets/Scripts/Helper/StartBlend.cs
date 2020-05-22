using UnityEngine;
using UnityEngine.UI;

public class StartBlend : MonoBehaviour
{
    public GameObject fadeTexturesObject;
    public Image fadeImage;
    public float fadeTime = 1.0f;

    private Timer fadeTimer;
    private float currentAlpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        fadeTimer = new Timer();
        fadeTimer.Start();

        fadeTexturesObject.SetActive(true);
        SetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        currentAlpha = 1f - (fadeTimer.Get() / fadeTime);

        if (currentAlpha <= 0f)
        {
            currentAlpha = 0f;
            SetAlpha();

            fadeTexturesObject.SetActive(false);

            fadeTimer = null;
            Destroy(this);
            return;
        }

        SetAlpha();
    }

    private void SetAlpha()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, currentAlpha);
    }
}
