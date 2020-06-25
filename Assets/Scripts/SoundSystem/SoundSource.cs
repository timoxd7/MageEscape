using UnityEngine;

public class SoundSource : MonoBehaviour
{
    [Tooltip("To Update the Values according to the SoundProperty eacht Frame, enable this. Else, only on Playing and complete Change of the SoundProperty, the Change will be applied. Needed for animations.")]
    public bool continouslyUpdate = false;
    public bool loopPlay = false;

    SoundSystem.SoundType soundType = SoundSystem.SoundType.Default;
    SoundClip soundClip;
    SoundSystem soundSystem;

    AudioSource audioSource;
    SoundProperty soundProperty;
    bool started = false;

    bool paused = false;
    bool pausedAtDisable = false;


    public void Initialize(SoundSystem.SoundType soundType, SoundClip soundClip, SoundSystem soundSystem, bool loopPlay = false, bool continouslyUpdate = false, bool autoplay = false)
    {
        if (started)
        {
            Debug.LogError("Tryed to initialize SoundSource, but already playing! ", this);
            return;
        }

        if (soundClip == null)
        {
            Debug.LogError("soundClip is null! ", this);
            return;
        }

        if (soundSystem == null)
        {
            Debug.LogError("soundSystem is null! ", this);
            return;
        }


        this.soundType = soundType;
        this.soundClip = soundClip;
        this.soundSystem = soundSystem;

        this.loopPlay = loopPlay;
        this.continouslyUpdate = continouslyUpdate;

        if (autoplay)
            Play();
    }

    private void Update()
    {
        if (started)
        {
            if (!loopPlay)
            {
                if (!audioSource.isPlaying && !paused)
                {
                    // Cleanup
                    soundSystem.RemovePlayingSource(this);
                    Destroy(this);
                    return;
                }
            }

            if (continouslyUpdate)
            {
                UpdateAudioSource();
            }
        }
    }

    private void OnEnable()
    {
        if (pausedAtDisable)
        {
            Resume();
            pausedAtDisable = false;
        }
    }

    private void OnDisable()
    {
        if (!paused)
        {
            Pause();
            pausedAtDisable = true;
        }
    }

    private void OnDestroy()
    {
        Destroy(audioSource);
    }

    public void UpdateProperty(SoundProperty soundProperty)
    {
        this.soundProperty = soundProperty;
        UpdateAudioSource();
    }

    public SoundSystem.SoundType GetSoundType()
    {
        return soundType;
    }

    public void Play()
    {
        if (started)
        {
            if (paused)
            {
                Resume();
            } else
            {
                Debug.LogError("Already Playing! ", this);
            }

            return;
        }

        if (soundClip == null || soundSystem == null || soundClip.audioClip == null)
        {
            Debug.LogError("Asked for playing SoundSource, but no null is given! ", this);
            Destroy(this);
            return;
        }
        
        // Create Sound Source
        audioSource = soundClip.GetSourceObject().AddComponent<AudioSource>();
        audioSource.clip = soundClip.audioClip;
        audioSource.loop = loopPlay;

        // Apply Sound Property
        soundProperty = soundSystem.GetProperty(soundType);
        UpdateAudioSource();

        // Start Playing
        soundSystem.AddPlayingSource(this);
        audioSource.Play();
        started = true;
    }

    public void Pause()
    {
        if (started)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                paused = true;
            }
        }
    }

    public void Resume()
    {
        if (paused)
        {
            audioSource.UnPause();
            paused = false;
        }
    }

    private void UpdateAudioSource()
    {
        soundProperty.Apply(audioSource);
        audioSource.loop = loopPlay;
        audioSource.volume *= soundClip.volumeMultiplyer;
    }
}
