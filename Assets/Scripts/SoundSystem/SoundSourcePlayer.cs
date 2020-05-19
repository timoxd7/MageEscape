using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourcePlayer : MonoBehaviour
{
    [Header("Sound System")]
    public SoundSystem.SoundType soundType = SoundSystem.SoundType.Default;
    public SoundSystem soundSystem;

    [Header("Sound Clips")]
    public List<SoundClip> soundClips;
    public bool destroyIfNoClip = true;

    [Header("Settings")]
    [Tooltip("Play the Sound on PlaySound() only once, then destroy this")]
    public bool playOnlyOnce = false;
    public bool playOnStart = false;
    public bool loopPlay = false;
    [Tooltip("To Update the Values according to the SoundProperty eacht Frame, enable this. Else, only on Playing and complete Change of the SoundProperty, the Change will be applied. Needed for animations.")]
    public bool continouslyUpdate = false;

    private bool oncePlayStarted = false;
    private List<SoundSource> soundSources;
    private bool paused = false;
    private bool pausedAtDisable = false;


    private void Awake()
    {
        if (soundClips.IsNullOrEmpty() && destroyIfNoClip)
        {
            Debug.Log("No SoundClip(s) attatched, destroying now: ", this);
            Destroy(this);
        }
    }

    private void Start()
    {
        if (playOnStart)
        {
            Play();
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
        if (oncePlayStarted)
        {
            foreach (SoundSource soundSource in soundSources)
            {
                if (soundSource != null)
                    Destroy(soundSource);
            }
        }
    }

    private void Update()
    {
        if (playOnlyOnce && oncePlayStarted)
        {
            bool stillPlaying = false;

            foreach (SoundSource soundSource in soundSources)
            {
                if (soundSource != null)
                {
                    stillPlaying = true;
                    break;
                }
            }

            if (!stillPlaying)
            {
                Destroy(this);
            }
        }
    }

#if UNITY_EDITOR
    [ButtonMethod]
#endif
    public void Play()
    {
        if (paused)
        {
            Resume();
        }

        if (soundSystem == null)
        {
            Debug.LogError("Sound System not given! ", this);

            if (playOnlyOnce)
                Destroy(this);

            return;
        }
        

        if (!oncePlayStarted)
        {
            oncePlayStarted = true;
            soundSources = new List<SoundSource>();
        }

        foreach (SoundClip soundClip in soundClips)
        {
            if (soundClip != null)
            {
                SoundSource soundSource = soundClip.GetSourceObject().AddComponent<SoundSource>();
                soundSource.Initialize(soundType, soundClip, soundSystem, loopPlay, continouslyUpdate, true);

                soundSources.Add(soundSource);
            }
        }
    }

#if UNITY_EDITOR
    [ButtonMethod]
#endif
    public void Pause()
    {
        if (oncePlayStarted && !paused)
        {
            foreach (SoundSource soundSource in soundSources)
            {
                if (soundSource != null)
                    soundSource.Pause();
            }

            paused = true;
        }
    }

#if UNITY_EDITOR
    [ButtonMethod]
#endif
    public void Resume()
    {
        if (paused)
        {
            foreach (SoundSource soundSource in soundSources)
            {
                if (soundSource != null)
                    soundSource.Resume();
            }

            paused = false;
        }
    }

#if UNITY_EDITOR
    [ButtonMethod]
    private void ForceKill()
    {
        Destroy(this);
    }
#endif
}
