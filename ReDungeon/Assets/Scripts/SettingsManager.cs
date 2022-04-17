using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public SettingsMenu settingsMenu;

    private const string saveFileName = "SettingsSave.dat";
    private const string master = "masterVolume";
    private const string music = "musicVolume";
    private const string sounds = "soundsVolume";
    

    public Resolution[] resolutions;
    public static SettingsManager instance;

    public float map(float x, float in_min, float in_max, float out_min, float out_max) => (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        resolutions = Screen.resolutions.Where(x => x.width >= 800 && x.height >= 600).ToArray();
    }

    private void Start()
    {
        LoadJsonData();
    }


    private void OnDestroy()
    {
        SaveJsonData();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(master, Mathf.Log10(volume) * 20);

        SetMusicVolume(settingsMenu.musicSlider.value);
        SetSoundsVolume(settingsMenu.soundsSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(music, Mathf.Log10(map(volume, 0.0001f, 1f, 0.0001f, settingsMenu.masterSlider.value)) * 20);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat(sounds, Mathf.Log10(map(volume, 0.0001f, 1f, 0.0001f, settingsMenu.masterSlider.value)) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    private void SaveJsonData()
    {
        SettingsSaveData sd = new SettingsSaveData();
        PopulateSavaData(sd);

        File.WriteAllText(saveFileName, sd.ToJson());
    }

    public void LoadJsonData()
    {
        if (File.Exists(saveFileName))
        {
            SettingsSaveData sd = new SettingsSaveData();
            sd.LoadFromJson(saveFileName);
            LoadFromSaveData(sd);
        }
        else
        {
            SaveDefaultJsonData();
            LoadJsonData();
        }
    }

    private void PopulateSavaData(SettingsSaveData settingsSaveData)
    {
        settingsSaveData.masterVolume = settingsMenu.masterSlider.value;
        settingsSaveData.musicVolume = settingsMenu.musicSlider.value;
        settingsSaveData.soundsVolume = settingsMenu.soundsSlider.value;

        settingsSaveData.isFullScreen = Screen.fullScreen;

        settingsSaveData.resolution = resolutions[settingsMenu.resolutionDropdown.value].ToString();

        settingsSaveData.qualityIndex = QualitySettings.GetQualityLevel();
    }

    private void SaveDefaultJsonData()
    {
        SettingsSaveData sd = new SettingsSaveData();
        PopulateDefaultSavaData(sd);

        File.WriteAllText(saveFileName, sd.ToJson());
    }

    private void PopulateDefaultSavaData(SettingsSaveData settingsSaveData)
    {
        settingsSaveData.masterVolume = 0.6f;
        settingsSaveData.musicVolume = 0.7f;
        settingsSaveData.soundsVolume = 0.5f;

        settingsSaveData.isFullScreen = true;

        settingsSaveData.resolution = resolutions.Last().ToString();

        settingsSaveData.qualityIndex = 3;
    }

    private void LoadFromSaveData(SettingsSaveData settingsSaveData)
    {
        settingsMenu.musicSlider.value = settingsSaveData.musicVolume;
        settingsMenu.soundsSlider.value = settingsSaveData.soundsVolume;
        settingsMenu.masterSlider.value = settingsSaveData.masterVolume;

        SetMasterVolume(settingsSaveData.masterVolume);

        SetFullScreen(settingsSaveData.isFullScreen);

        int[] temp = settingsSaveData.resolution.Split(new char[] { ' ', 'x' }, System.StringSplitOptions.RemoveEmptyEntries).Take(2).Select(x => int.Parse(x)).ToArray();

        settingsMenu.resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == temp[0] &&
                  resolutions[i].height == temp[1])
            {
                currentResolutionIndex = i;
            }

        }

        settingsMenu.resolutionDropdown.AddOptions(options);
        settingsMenu.resolutionDropdown.value = currentResolutionIndex;
        settingsMenu.resolutionDropdown.RefreshShownValue();


        SetResolution(temp[0], temp[1]);
        SetQuality(settingsSaveData.qualityIndex);
    }

}

public class SettingsSaveData
{
    public float masterVolume;
    public float musicVolume;
    public float soundsVolume;

    public bool isFullScreen;
    public string resolution;
    public int qualityIndex;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string file)
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(file), this);
    }
}