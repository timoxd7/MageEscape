using UnityEngine;

public abstract class BaseDetection : MonoBehaviour, IDetectable
{
    public abstract void OnDetectionEnter();
    public abstract void OnDetectionExit();
}
