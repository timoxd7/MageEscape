using UnityEngine;

public class StaticSoundProperty : SoundProperty
{
    [Header("Property Auto Assignment")]
    public SoundSystem soundSystem;
    public SoundSystem.SoundType soundType = SoundSystem.SoundType.Default;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (soundSystem == null)
        {
            Debug.LogError("soundSystem is null! ", this);
            Destroy(this);
            return;
        }

        soundSystem.UpdateProperty(this, soundType);
    }
}
