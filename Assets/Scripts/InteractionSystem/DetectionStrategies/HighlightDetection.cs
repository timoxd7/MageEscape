using MyBox;
using UnityEngine;

public class HighlightDetection : BaseDetection
{

    public Outline.Mode outlineMode = Outline.Mode.OutlineVisible;
    public Color outlineColor = Color.white;
    public float outlineWidth = 5f;

    private Outline outlineInstance;

    private void Start()
    {
        outlineInstance = gameObject.GetOrAddComponent<Outline>();
        outlineInstance.enabled = false;
    }

    public override void OnDetectionEnter()
    {
        if (outlineInstance == null)
            outlineInstance = gameObject.GetOrAddComponent<Outline>();

        if (outlineInstance != null)
        {
            outlineInstance.enabled = true;
            outlineInstance.OutlineMode = outlineMode;
            outlineInstance.OutlineColor = outlineColor;
            outlineInstance.OutlineWidth = outlineWidth;
        } else
        {
            Debug.LogError("Can't create or locate outline Instance", gameObject);
        }
    }

    public override void OnDetectionExit()
    {
        if (outlineInstance == null)
            outlineInstance = gameObject.GetComponent<Outline>();

        if (outlineInstance != null)
            outlineInstance.enabled = false;

        outlineInstance = null;
    }
}
