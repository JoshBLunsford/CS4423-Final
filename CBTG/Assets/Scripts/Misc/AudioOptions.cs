using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptions : MonoBehaviour
{
    public enum AudioType { Music, SoundEffects }
    public AudioType audioType;
    public static float mVolume = 1, seVolume = 1;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource.volume = GetVolume(audioType);
    }

    public static float GetVolume(AudioType audioType)
    {
        float volume = audioType switch
        {
            AudioType.Music => mVolume,
            AudioType.SoundEffects => seVolume,
            _ => 0,
        };
        return volume;
    }

}
