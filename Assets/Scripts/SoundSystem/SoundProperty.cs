using MyBox;
using UnityEngine;
using UnityEngine.Audio;

public class SoundProperty : MonoBehaviour
{
    public bool useFromAudioSource;
    [ConditionalField(nameof(useFromAudioSource))]
    public AudioSource audioSource;

    [ConditionalField(nameof(useFromAudioSource), true)]
    [Header("Audio Source Settings")]
    public AudioMixerGroup output;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public bool mute = false;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public bool bypassEffects = false;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public bool bypassListenerEffects = false;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public bool bypassReverbZones = false;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0, 256)]
    public int priority = 128;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0f, 1f)]
    public float volume = 1.0f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0f, 1f)]
    public float pitch = 1.0f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(-1f, 1f)]
    public float stereoPan = 0f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0f, 1f)]
    public float spatialBlend = 1f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0f, 1.1f)]
    public float reverbZoneMix = 1f;

    
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0f, 5f)]
    [Header("3D Sound Settings")]
    public float dopplerLevel = 1f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    [Range(0, 360)]
    public float spread = 0f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public AudioRolloffMode volumeRolloff = AudioRolloffMode.Logarithmic;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public float minDistance = 1f;
    [ConditionalField(nameof(useFromAudioSource), true)]
    public float maxDistance = 500f;

    // Private because this is set by the SoundSource itself
    private bool playOnAwake = false;
    private bool loop = false;

    public void Apply(AudioSource audioSource)
    {
        if (useFromAudioSource)
        {
            if (this.audioSource == null)
            {
                Debug.LogError("Asked to copy Values from AudioSource, but it's null! ", this);
                return;
            }

            output = this.audioSource.outputAudioMixerGroup;
            mute = this.audioSource.mute;
            bypassEffects = this.audioSource.bypassEffects;
            bypassListenerEffects = this.audioSource.bypassListenerEffects;
            bypassReverbZones = this.audioSource.bypassReverbZones;
            playOnAwake = this.audioSource.playOnAwake;
            loop = this.audioSource.loop;
            priority = this.audioSource.priority;
            volume = this.audioSource.volume;
            pitch = this.audioSource.pitch;
            stereoPan = this.audioSource.panStereo;
            spatialBlend = this.audioSource.spatialBlend;
            reverbZoneMix = this.audioSource.reverbZoneMix;
            dopplerLevel = this.audioSource.dopplerLevel;
            spread = this.audioSource.spread;
            volumeRolloff = this.audioSource.rolloffMode;
            minDistance = this.audioSource.minDistance;
            maxDistance = this.audioSource.maxDistance;
        }

        audioSource.outputAudioMixerGroup = output;
        audioSource.mute = mute;
        audioSource.bypassEffects = bypassEffects;
        audioSource.bypassListenerEffects = bypassListenerEffects;
        audioSource.bypassReverbZones = bypassReverbZones;
        audioSource.playOnAwake = playOnAwake;
        audioSource.loop = loop;
        audioSource.priority = priority;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.panStereo = stereoPan;
        audioSource.spatialBlend = spatialBlend;
        audioSource.reverbZoneMix = reverbZoneMix;
        audioSource.dopplerLevel = dopplerLevel;
        audioSource.spread = spread;
        audioSource.rolloffMode = volumeRolloff;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
    }
}
