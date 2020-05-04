using UnityEngine;

public class ConsoleDetection : BaseDetection

{
    public override void OnDetectionEnter()
    {
        Debug.Log("Detection Enter: " + gameObject.name);
    }

    public override void OnDetectionExit()
    {
        Debug.Log("Detection Exit: " + gameObject.name);
    }
}