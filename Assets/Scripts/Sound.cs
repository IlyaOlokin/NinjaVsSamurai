using System;
using UnityEngine.Audio;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(0, 3)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
