using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanged : MonoBehaviour
{
    //Script has to be where AudioSources are
    AudioSource[] asc;

    void Start()
    {
        asc = GetComponents<AudioSource>();
    }

    public void SetVolume(float vol)
    {
        for (int i = 0; i < asc.Length; i++)
        {
            asc[i].volume = vol;
        }
    }
}
