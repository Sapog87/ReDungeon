using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string Name;
    public AudioClip Clip;
    public bool Loop;

    [HideInInspector]
    public AudioSource source;
}
