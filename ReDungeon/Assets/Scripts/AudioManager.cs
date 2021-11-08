using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Audio[] audios;

    public static AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.Clip;
            audio.source.loop = audio.Loop;
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(params string[] names)
    {
        AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        if (audioMixerGroup == null)
            return;

        foreach (string name in names)
        {
            Audio next = Array.Find(audios, audio => audio.Name == name);
            if (next != null)
            {
                next.source.outputAudioMixerGroup = audioMixerGroup;
                next.source.Play();
            }
        }
    }

    public void PlaySounds(params string[] names)
    {
        AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Sounds")[0];
        if (audioMixerGroup == null)
            return;
        
        foreach (string name in names)
        {
            Audio next = Array.Find(audios, audio => audio.Name == name);
            if (next != null)
            {
                next.source.outputAudioMixerGroup = audioMixerGroup;
                next.source.Play();
            }
        }
    }

    public void PlayMaster(params string[] names)
    {
        AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        if (audioMixerGroup == null)
            return;

        foreach (string name in names)
        {
            Audio next = Array.Find(audios, audio => audio.Name == name);
            if (next != null)
            {
                next.source.outputAudioMixerGroup = audioMixerGroup;
                next.source.Play();
            }
        }
    }

    public void StopPlaying(string name)
    {
        Audio next = Array.Find(audios, audio => audio.Name == name);
        if (next == null)
            return;
        
        next.source.Stop();
    }

    public void StopAllAudio()
    {
        foreach(Audio audio in audios)
            if (audio.source.isPlaying)
                audio.source.Stop();
    }

    public bool IsPlaying(string name)
    {
        Audio next = Array.Find(audios, audio => audio.Name == name);
        return next.source.isPlaying;
    }

    public void SetPlayTiming(string name, float seconds)
    {
        Audio next = Array.Find(audios, audio => audio.Name == name);
        next.source.time = seconds;
    }
}
