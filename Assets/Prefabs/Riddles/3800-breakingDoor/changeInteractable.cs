using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeInteractable : MonoBehaviour
{
    public bool changeTo;
    public GameObject sceneObject;
    void change()
    {
        sceneObject.GetComponent<Interactable>().isInteractable = changeTo;
        Destroy(sceneObject.GetComponent<Interactable>().gameObject.GetComponent<HighlightDetection>()); 
        sceneObject.GetComponent<Interactable>().detectionOption = DetectionOption.ConsoleLog;
        
    }
}