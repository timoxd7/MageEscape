using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    private Dictionary<SoundType, List<SoundSource>> playingSources = new Dictionary<SoundType, List<SoundSource>>();
    private Dictionary<SoundType, SoundProperty> soundProperties = new Dictionary<SoundType, SoundProperty>();


    public void UpdateProperty(SoundProperty soundProperty, SoundType soundType)
    {
        if (!soundProperties.ContainsKey(soundType))
        {
            soundProperties.Add(soundType, null);
        }

        soundProperties[soundType] = soundProperty;

        if (playingSources.ContainsKey(soundType))
        {
            foreach (SoundSource soundSource in playingSources[soundType])
            {
                soundSource.UpdateProperty(soundProperty);
            }
        }
    }

    public SoundProperty GetProperty(SoundType soundType)
    {
        if (!soundProperties.ContainsKey(soundType))
        {
            // Create default property
            soundProperties.Add(soundType, new SoundProperty());
        }

        return soundProperties[soundType];
    }

    public void AddPlayingSource(SoundSource soundSource)
    {
        if (soundSource != null)
        {
            SoundType soundType = soundSource.GetSoundType();
            
            if (!playingSources.ContainsKey(soundType))
            {
                playingSources.Add(soundType, new List<SoundSource>());
            }

            playingSources[soundType].Add(soundSource);
        } else
            Debug.LogError("Got null as soundSource! ", this);
    }

    public void RemovePlayingSource(SoundSource soundSource)
    {
        if (soundSource != null)
        {
            SoundType soundType = soundSource.GetSoundType();

            if (playingSources.ContainsKey(soundType))
            {
                playingSources[soundType].Remove(soundSource);
            }
        } else
            Debug.LogError("Got null as soundSource! ", this);
    }


    #region Enums

    public enum SoundType {
        Default,
        Player,
        Envoirment,
        Ambient,
        Explosion
    }

    #endregion
}
