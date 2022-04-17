using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public SettingsManager settingsManager;
    public Dropdown graphicsQualityDropdown, resolutionDropdown;
    public Slider masterSlider, musicSlider, soundsSlider;

    public Scrollbar scrollbar;

    private void Start()
    {
        scrollbar.value = 1;

        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetMasterVolume(float volume)
    {
        settingsManager.SetMasterVolume(volume);
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
        settingsManager.SetQuality(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        settingsManager.SetFullScreen(isFullScreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        settingsManager.SetResolution(settingsManager.resolutions[resolutionIndex]);
    }

    public void SetResolution(int width, int height)
    {
        settingsManager.SetResolution(width, height);
    }
}
