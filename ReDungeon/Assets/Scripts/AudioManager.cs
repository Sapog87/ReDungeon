using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Audio[] audios;
    //private AudioMixerGroup MusicGroup = audioMixer.FindMatchingGroups("Music")[0];

    public static AudioManager instance;

    const float timeToFade = 1.25f;

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

            switch (audio.MixerGroup)
            {
                case (MixerGroup.Music): audio.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0]; break;
                case (MixerGroup.Sounds): audio.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sounds")[0]; break;
            }
        }
    }

    public void PlayMusic(string name, float volume = 1f, int timing = 0)
    {
        AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        if (audioMixerGroup == null)
            return;

        Audio next = Array.Find(audios, audio => audio.Name == name);
        if (next != null)
        {
            next.source.outputAudioMixerGroup = audioMixerGroup;
            next.source.volume = volume;
            next.source.time = timing;
            next.source.Play();
        }
    }

    public void PlaySounds(string name, float volume = 1f, int timing = 0)
    {
        AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Sounds")[0];
        if (audioMixerGroup == null)
            return;

        Audio next = Array.Find(audios, audio => audio.Name == name);
        if (next != null)
        {
            next.source.outputAudioMixerGroup = audioMixerGroup;
            next.source.volume = volume;
            next.source.time = timing;
            next.source.Play();
        }
    }

    public void PlayMaster(string name, float volume = 1f, int timing = 0)
    {
        AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        if (audioMixerGroup == null)
            return;

        Audio next = Array.Find(audios, audio => audio.Name == name);
        if (next != null)
        {
            next.source.outputAudioMixerGroup = audioMixerGroup;
            next.source.volume = volume;
            next.source.time = timing;
            next.source.Play();
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

    private IEnumerator FadeChange(AudioSource oldAudio, AudioSource nextAudio, float newTrackVolume)
    {
        float timeElapsed = 0;

        nextAudio.Play();
        float a = oldAudio.volume;

        while (timeElapsed < timeToFade)
        {
            oldAudio.volume = Mathf.Lerp(a, 0, timeElapsed / timeToFade);
            nextAudio.volume = Mathf.Lerp(0, newTrackVolume, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        oldAudio.Pause();
    }

    private IEnumerator Fade(AudioSource audio)
    {
        float timeElapsed = 0;

        float a = audio.volume;

        while (timeElapsed < timeToFade)
        {
            audio.volume = Mathf.Lerp(a, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        audio.Pause();
    }

    private IEnumerator Unfade(AudioSource audio, float newTrackVolume)
    {
        float timeElapsed = 0;

        audio.Play();

        while (timeElapsed < timeToFade)
        {
            audio.volume = Mathf.Lerp(0, newTrackVolume, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void SmoothTrackChange(string oldTrackName, string newTrackName, float newTrackVolume = 0.8f, int newTrackTiming = 0)
    {
        AudioSource oldAudio = Array.Find(audios, audio => audio.Name == oldTrackName).source;
        AudioSource nextAudio = Array.Find(audios, audio => audio.Name == newTrackName).source;

        if (oldAudio == null || nextAudio == null)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeChange(oldAudio, nextAudio, newTrackVolume));
    }

    public void SmoothTrackFade(string trackName)
    {
        AudioSource audio = Array.Find(audios, audio => audio.Name == trackName).source;

        if (audio == null)
            return;

        StopAllCoroutines();
        StartCoroutine(Fade(audio));
    }

    public void SmoothTrackUnfade(string trackName, float newTrackVolume)
    {
        AudioSource audio = Array.Find(audios, audio => audio.Name == trackName).source;

        if (audio == null)
            return;

        StopAllCoroutines();
        StartCoroutine(Unfade(audio, newTrackVolume));
    }

}
