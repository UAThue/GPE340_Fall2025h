using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class PauseMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown screenResolutionDropdown;
    public TMP_Dropdown graphicSettingsDropdown;

    public void Start()
    {
        // Load our settings and options into the dropdowns!
        FillResolutionSettingsDropdown();

        // TODO: Fill Graphics Settings Dropdown

        // Set our volume to our current settings
        LoadSoundMixerVolumes();

        // TODO: Set the dropdown to the current settings

    }


    private void SaveSoundMixerVolumes()
    {
        // Save to player prefs
        PlayerPrefs.SetFloat("MasterVolume", mainVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", soundEffectsVolumeSlider.value);
        PlayerPrefs.Save(); // Write to disk
    }

    private void UpdateMixersBasedOnSliders()
    {
        // Update the mixers to match
        GameManager.instance.audioMixer.SetFloat("MasterVolume", ConvertFromLinearSliderToDecibels(mainVolumeSlider.value));
        GameManager.instance.audioMixer.SetFloat("MusicVolume", ConvertFromLinearSliderToDecibels(musicVolumeSlider.value));
        GameManager.instance.audioMixer.SetFloat("EffectsVolume", ConvertFromLinearSliderToDecibels(soundEffectsVolumeSlider.value));
    }

    private void LoadSoundMixerVolumes()
    {
        // THIS METHOD SETS SLIDER VALUES BASED ON THE SAVE PLAYERPREFS (Save Game) DATA IF AVAILABLE, OTHERWISE USE MIXER VALUES
        float value;
        
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            mainVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MasterVolume"));
        } else
        {
            GameManager.instance.audioMixer.GetFloat("MasterVolume", out value);
            mainVolumeSlider.SetValueWithoutNotify(ConvertFromDecibelsToLinearSlider(value));
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MusicVolume"));
        } else
        {
            GameManager.instance.audioMixer.GetFloat("MusicVolume", out value);
            musicVolumeSlider.SetValueWithoutNotify(ConvertFromDecibelsToLinearSlider(value));
        }

        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            soundEffectsVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("EffectsVolume"));
        } else
        {
            GameManager.instance.audioMixer.GetFloat("EffectsVolume", out value);
            soundEffectsVolumeSlider.SetValueWithoutNotify(ConvertFromDecibelsToLinearSlider(value));
        }
    }

    private float ConvertFromLinearSliderToDecibels( float linearValue )
    {
        return Mathf.Log10(linearValue) * 20;
    }

    private float ConvertFromDecibelsToLinearSlider ( float decibelValue )
    {
        return Mathf.Pow(10.0f, decibelValue / 20);
    }

    //****************************
    // Function: FillResolutionSettingsDropdown()
    // Purpose: Adds all possible resolutions to the dropdown for Graphic Resolution Settings
    //****************************

    private void FillResolutionSettingsDropdown()
    {
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutionOptions.Add(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height + " : " + Screen.resolutions[i].refreshRateRatio);
        }
        // Clear the dropdown
        screenResolutionDropdown.ClearOptions();

        // Add those options to the dropdown
        screenResolutionDropdown.AddOptions(resolutionOptions);
    }

    public void OnMainVolumeChange()
    {
        UpdateMixersBasedOnSliders();
        SaveSoundMixerVolumes();
    }

    public void OnSFXVolumeChange()
    {
        UpdateMixersBasedOnSliders();
        SaveSoundMixerVolumes();
    }

    public void OnMusicVolumeChange()
    {
        UpdateMixersBasedOnSliders();
        SaveSoundMixerVolumes();
    }

    public void OnResumeGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.Unpause();
        }
    }

    public void OnQuitToMenu()
    {
        //TODO: Connect to main menu. For now, this is our start screen
        SceneManager.LoadScene("PressStart");
    }

    public void OnGraphicsSettingChange()
    {
        // TODO: Change graphics settings!
    }

    public void OnScreenResolutionChange()
    {
        int index = screenResolutionDropdown.value;
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, fullscreenToggle.isOn);
    }

}
