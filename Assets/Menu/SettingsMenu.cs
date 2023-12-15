using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro.EditorUtilities;
using TMPro;
using UnityEngine.PlayerLoop;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    private static SettingsMenu instance;
    public bool isPaused;

    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        settingsMenu.SetActive(false);

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }
  
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
/*
    public void PauseGame()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResumeGame() 
    {
        settingsMenu?.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }*/

    public void ToggleMenu(bool isActive)
    {
        Debug.Log($"Toggling menu. Active: {isActive}");
        settingsMenu.SetActive(isActive);
    }
}
