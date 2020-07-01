using UnityEngine;
using UnityEngine.UI;

public class BlackBlend : MonoBehaviour
{
    public GameObject fadeTexturesObject;
    public Image fadeImage;
    public float fadeTime = 1.0f;
    public bool fadeOnStart = true;

    private float currentAlpha = 1.0f;
    private bool currentSetState = false;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeTexturesObject == null)
        {
            Debug.LogError("No fadeTextureObject given!", this);
            Destroy(this);
            return;
        }

        if (fadeImage == null)
        {
            Debug.LogError("No fadeImage given!", this);
            Destroy(this);
            return;
        }

        if (fadeOnStart)
        {
            currentAlpha = 1.0f;
            fadeTexturesObject.SetActive(true);
        }
        else
        {
            currentAlpha = 0.0f;
            fadeTexturesObject.SetActive(false);
        }

        SetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSetState)
        {
            // Fade to Black
            if (currentAlpha != 1.0f)
            {
                if (!fadeTexturesObject.activeSelf)
                {
                    fadeTexturesObject.SetActive(true);
                }

                currentAlpha += Time.deltaTime / fadeTime;

                if (currentAlpha > 1.0f)
                    currentAlpha = 1.0f;

                SetAlpha();
            }
        } else
        {
            // Fade from Black
            if (currentAlpha != 0.0f)
            {
                if (!fadeTexturesObject.activeSelf)
                {
                    fadeTexturesObject.SetActive(false);
                }

                currentAlpha -= Time.deltaTime / fadeTime;

                if (currentAlpha <= 0.0f)
                {
                    currentAlpha = 0.0f;
                    fadeTexturesObject.SetActive(false);
                }

                SetAlpha();
            }
        }
    }

    public void SetBlack(bool state)
    {
        currentSetState = state;
    }

    private void SetAlpha()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, currentAlpha);
    }
}
