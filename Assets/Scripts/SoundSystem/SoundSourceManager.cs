using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourceManager : MonoBehaviour
{
    public SoundSystem.SoundType soundType = SoundSystem.SoundType.Default;
    public SoundSystem soundSystem;

    public List<SoundClip> soundClips;
    public bool destroyIfNoClip = true;

    [Tooltip("Play the Sound on PlaySound() only once, then destroy this")]
    public bool playOnlyOnce = false;

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

    public void Pause()
    {
        if (oncePlayStarted && !paused)
        {
            foreach (SoundSource soundSource in soundSources)
            {
                if (soundSource != null)
                    soundSource.Pause();
            }
        }
    }

    public void Resume()
    {
        if (oncePlayStarted && paused)
        {
            foreach (SoundSource soundSource in soundSources)
            {
                if (soundSource != null)
                    soundSource.Resume();
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
                soundSource.Initialize(soundType, soundClip, soundSystem, true);

                soundSources.Add(soundSource);
            }
        }
    }
}
