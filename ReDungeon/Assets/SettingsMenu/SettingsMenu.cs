using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public SettingsManager settingsManager;
    public Dropdown graphicsQualityDropdown, resolutionDropdown;
    public Slider masterSlider, musicSlider, soundsSlider;

    public Scrollbar scrollbar;

    Resolution[] resolutions;

    private void Start()
    {
        scrollbar.value = 1;

        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    string option = resolutions[i].width + " x " + resolutions[i].height;
        //    options.Add(option);

        //    if (resolutions[i].width == Screen.currentResolution.width &&
        //       resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}
        foreach (var item in resolutions)
        {
            options.Add(item.ToString());
            currentResolutionIndex++;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        settingsManager.SetMasterVolume(volume, musicSlider.value, soundsSlider.value);
    }

    public void SetMusicVolume(float percentage)
    {
        settingsManager.SetMusicVolume(percentage);
    }

    public void SetSoundsVolume(float percentage)
    {
        settingsManager.SetSoundsVolume(percentage);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
