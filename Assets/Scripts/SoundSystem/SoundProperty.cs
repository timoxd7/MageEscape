using MyBox;
using UnityEngine;

public class SoundProperty : MonoBehaviour
{
    public bool useFromAudioSource;
    [ConditionalField(nameof(useFromAudioSource))]
    public AudioSource audioSource;

    [ConditionalField(nameof(useFromAudioSource), true)]
    public float volume = 1.0f;

    public void Apply(AudioSource audioSource)
    {
        if (useFromAudioSource)
        {
            volume = this.audioSource.volume;
        }

        audioSource.volume = volume;
    }
}
