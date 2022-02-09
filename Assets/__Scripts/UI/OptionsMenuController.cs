using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    // Audio
    public AudioMixer audioMixer;
    private string _globalAudioParam = "globalVolume";

    // Graphics
    private Resolution[] _resolutions;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    public UserSettings userSettings;

    // UI refs
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _graphicsQualityDropdown;

    private void Start() 
    {
        // Need to refactor the way resolution is set
        _resolutions = Screen.resolutions; 
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex; // Set res to current screen resolution of user
        _resolutionDropdown.RefreshShownValue();
    }

    #region Helpers
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(_globalAudioParam, Mathf.Log10(volume) * 20);
        userSettings.globalVolume = volume;
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        userSettings.graphicsQualityIndex = qualityIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        userSettings.isFullscreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        userSettings.screenResolution.width = resolution.width;
        userSettings.screenResolution.height = resolution.height;
    }

    public void SetOptionUIFromUserSettings()
    {
        _audioSlider.value = userSettings.globalVolume;
        _fullscreenToggle.isOn = userSettings.isFullscreen;
        _graphicsQualityDropdown.value = userSettings.graphicsQualityIndex;

        // Resolution is currently not implemented
    }
    
    #endregion
}
