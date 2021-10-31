using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider, musicSlider, soundsSlider;
    public Dropdown graphicsQualityDropdown, resolutionDropdown;

    Resolution[] resolutions;


    private void Start()
    {
        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        audioMixer.SetFloat("musicVolume", masterSlider.minValue + (volume + masterSlider.maxValue - masterSlider.minValue) * musicSlider.value);
        audioMixer.SetFloat("soundsVolume", masterSlider.minValue + (volume + masterSlider.maxValue - masterSlider.minValue) * soundsSlider.value);
    }

    public void SetMusicVolume(float percentage)
    {
        float volume;
        audioMixer.GetFloat("masterVolume", out volume);
        audioMixer.SetFloat("musicVolume", masterSlider.minValue + (volume + masterSlider.maxValue - masterSlider.minValue) * percentage);
    }

    public void SetSoundsVolume(float percentage)
    {
        float volume;
        audioMixer.GetFloat("masterVolume", out volume);
        audioMixer.SetFloat("soundsVolume", masterSlider.minValue + (volume + masterSlider.maxValue - masterSlider.minValue) * percentage);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
