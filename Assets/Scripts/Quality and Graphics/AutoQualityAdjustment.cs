using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoQualityAdjustment : MonoBehaviour
{

    // Frame Counter Stuff
    [Header("FPS Info")]
    private int frameCount = 0;
    private float dt = 0f;
    private float updateRate = 4f;  // 4 updates per sec.
    [SerializeField]
    private float fps = 0f;
    public bool showFpsIngame = false;

    // Adjustment Check Stuff
    [Header("Quality Adjustment Settings")]
    public float fpsUpperThreshhold = 70f; // 70 FPS maximum
    public float fpsLowerThreshhold = 50f; // 50 FPS minimum
    public float maxTimeOutOfBound = 1f;
    [SerializeField]
    private bool highestQualityLocked = false;
    private Timer outOfBoundSince = new Timer();
    private bool outOfBoundOver = false;

    [Header("Info")]
    public bool showQualitySettingIngame = false;
    [SerializeField]
    private string currentQualitySetting;


    void Start()
    {
        currentQualitySetting = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }

    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1f / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1f / updateRate;

            CheckAdjustment();
        }
    }

    void OnGUI()
    {
        string onGuiInfo = "";

        if (showFpsIngame)
            onGuiInfo += fps.ToString("0");

        if (showFpsIngame && showQualitySettingIngame)
            onGuiInfo += " - ";

        if (showQualitySettingIngame)
            onGuiInfo += currentQualitySetting;

        if (!onGuiInfo.Equals(""))
            GUI.Label(new Rect(10, 10, 100, 20), onGuiInfo);
    }

    public float GetFPS()
    {
        return fps;
    }


    private void CheckAdjustment()
    {
        if (fps < fpsLowerThreshhold)
        {
            if (outOfBoundSince.GetStarted())
            {
                if (!outOfBoundOver)
                {
                    if (outOfBoundSince.Get() > maxTimeOutOfBound)
                    {
                        LowerQualitySettings();
                        outOfBoundSince.Reset();
                    }
                } else
                {
                    outOfBoundOver = false;
                    outOfBoundSince.Restart();
                }
            } else
            {
                outOfBoundOver = false;
                outOfBoundSince.Start();
            }
        } else if (fps > fpsUpperThreshhold) {
            if (outOfBoundSince.GetStarted())
            {
                if (outOfBoundOver)
                {
                    if (outOfBoundSince.Get() > maxTimeOutOfBound)
                    {
                        HigherQualitySettings();
                        outOfBoundSince.Reset();
                    }
                }
                else
                {
                    outOfBoundOver = true;
                    outOfBoundSince.Restart();
                }
            }
            else
            {
                outOfBoundOver = true;
                outOfBoundSince.Start();
            }
        } else
        {
            outOfBoundSince.Reset();
        }
    }

    private void LowerQualitySettings()
    {
        if (highestQualityLocked)
        {
            QualitySettings.DecreaseLevel(false);
        } else
        {
            highestQualityLocked = true;
            QualitySettings.DecreaseLevel(true);
        }

        currentQualitySetting = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }

    private void HigherQualitySettings()
    {
        int currentQualityLevel = QualitySettings.GetQualityLevel();

        if (highestQualityLocked)
        {
            if (QualitySettings.names.Length >= 2)
            {
                if (currentQualityLevel < QualitySettings.names.Length - 2)
                {
                    QualitySettings.IncreaseLevel(false);
                }
            }
        } else
        {
            QualitySettings.IncreaseLevel(true);
        }

        currentQualitySetting = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
}