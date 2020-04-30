using UnityEngine;

public class ConsoleDetection : MonoBehaviour, IDetectable

{
    public void OnDetectionEnter()
    {
        Debug.Log("Detection Enter: " + gameObject.name);
    }

    public void OnDetectionExit()
    {
        Debug.Log("Detection Exit: " + gameObject.name);
    }
}