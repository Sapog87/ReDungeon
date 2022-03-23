using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string Name;
    public AudioClip Clip;
    public MixerGroup MixerGroup;
    public bool Loop;

    [HideInInspector]
    public AudioSource source;
}

public enum MixerGroup
{
    Music,
    Sounds,
}
