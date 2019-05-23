using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour 
{
    public TMP_Dropdown resolutionDropDown;
    public TMP_Dropdown qualityDropDown;
    public Toggle toggle;

    Resolution[] resolutions;
    public AudioMixer audioMixer;

    private void Start()
    {
        resolutions = Screen.resolutions;
        SetResolutionDropDown();
        InitChoicesToCurrentSettings();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolutionDropDown()
    {
        List<string> screenResolutions = new List<string>();

        resolutionDropDown.ClearOptions();

        foreach(Resolution res in resolutions)
        {
            screenResolutions.Add(res.width + " x " + res.height);
        }

        resolutionDropDown.AddOptions(screenResolutions);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void InitChoicesToCurrentSettings()
    {
        toggle.isOn = Screen.fullScreen;
        qualityDropDown.value = QualitySettings.GetQualityLevel();
        qualityDropDown.RefreshShownValue();

        Resolution currentResolution = Screen.currentResolution;
        string resolution = currentResolution.width + " x " + currentResolution.height;

        int resolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].Equals(currentResolution))
            {
                resolutionIndex = i;
            }
        }

        resolutionDropDown.value = resolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
}
