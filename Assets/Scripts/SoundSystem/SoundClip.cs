using MyBox;
using UnityEngine;

public class SoundClip : MonoBehaviour
{
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volumeMultiplyer = 1.0f;

    [Tooltip("Use this GameObject as the Parent for this SoundClip")]
    public bool useThisObject = true;

    [ConditionalField(nameof(useThisObject), true)]
    public GameObject parentObject;

    private void Awake()
    {
        if (useThisObject)
            parentObject = gameObject;
    }

    public GameObject GetSourceObject()
    {
        if (useThisObject)
        {
            return gameObject;
        } else
        {
            if (parentObject == null)
            {
                Debug.LogError("Null Object Exception. Switching to Default = Parent Object! ", this);
                return gameObject;
            } else
            {
                return parentObject;
            }
        }
    }
}
