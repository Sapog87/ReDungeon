using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    const float defaultMasterVolume = 0.6f;
    const float defaultMusicMult = 0.7f;
    const float defaultSoundsMult = 0.5f;

    public static SettingsManager instance;

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
    }

    private float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private void Start()
    {
        SetMasterVolume(defaultMasterVolume, defaultMusicMult, defaultSoundsMult);
    }

    public void SetMasterVolume(float volume, float musicPercentage, float soundsPercentage)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        SetMusicVolume(musicPercentage);
        SetSoundsVolume(soundsPercentage);
    }

    public void SetMusicVolume(float volume)
    {
        float msVolume;
        audioMixer.GetFloat("masterVolume", out msVolume);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(map(volume, 0.0001f, 1f, 0.0001f, Mathf.Pow(10, msVolume / 20))) * 20);
    }

    public void SetSoundsVolume(float volume)
    {
        float msVolume;
        audioMixer.GetFloat("masterVolume", out msVolume);
        audioMixer.SetFloat("soundsVolume", Mathf.Log10(map(volume, 0.0001f, 1f, 0.0001f, Mathf.Pow(10, msVolume / 20))) * 20);
    }


}
