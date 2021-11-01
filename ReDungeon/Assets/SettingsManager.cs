using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public int minVolume = -40;
    public int maxVolume = 0;

    const float defaultMasterVolume = -10;
    const float defaultMusicMult = 0.7f;
    const float defaultSoundsMult = 0.5f;

    private void Start()
    {
        SetMasterVolume(defaultMasterVolume, defaultMusicMult, defaultSoundsMult);
    }

    public void SetMasterVolume(float volume, float musicPercentage, float soundsPercentage)
    {
        audioMixer.SetFloat("masterVolume", volume <= minVolume ? -80 : volume);

        SetMusicVolume(musicPercentage);
        SetSoundsVolume(soundsPercentage);
    }

    public void SetMusicVolume(float percentage)
    {
        float volume, newVolume;
        audioMixer.GetFloat("masterVolume", out volume);
        newVolume = minVolume + (volume + maxVolume - minVolume) * percentage;
        audioMixer.SetFloat("musicVolume", newVolume <= minVolume ? -80 : newVolume * 1.15f);
    }

    public void SetSoundsVolume(float percentage)
    {
        float volume, newVolume;
        audioMixer.GetFloat("masterVolume", out volume);
        newVolume = minVolume + (volume + maxVolume - minVolume) * percentage;
        audioMixer.SetFloat("soundsVolume", newVolume <= minVolume ? -80 : newVolume * 1.15f);
    }


}
