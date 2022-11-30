using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class SettingsScreen : MonoBehaviour
{
    public GameObject consoleCanvas;
    public GameObject settingsPanel;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI sensText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI qualityText;
    public Slider volumeSlider;
    public Slider sensSlider;
    public Slider levelSlider;
    public Slider qualitySlider;
    public Toggle toggleFps;
    public Toggle toggleVSync;
    void Awake()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpeningGame", 0) == 0)
        {
            SetDefaultSettings();
            PlayerPrefs.SetInt("FirstTimeOpeningGame", 1);
        }
    }
    public void SetDefaultSettings()
    {
        PlayerPrefs.SetFloat("MouseSens", 0.75f);
        PlayerPrefs.SetFloat("Volume", 0.8f);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        PlayerPrefs.SetInt("Quality", 5);
        PlayerPrefs.SetInt("ShowFPS", 0);
        PlayerPrefs.SetInt("VSync", 1);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            consoleCanvas.SetActive(!consoleCanvas.activeSelf);
        }
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        volumeText.text = (100 * PlayerPrefs.GetFloat("Volume")).ToString("F1");
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");

        sensText.text = PlayerPrefs.GetFloat("MouseSens").ToString("F2");
        sensSlider.value = PlayerPrefs.GetFloat("MouseSens");

        levelSlider.maxValue = PlayerPrefs.GetInt("UnlockedLevels");
        levelSlider.value = PlayerPrefs.GetInt("Level");
        if (levelSlider.value == 0)
        {
            levelText.text = "tutorial";
        }
        else if (levelSlider.value == SceneManager.sceneCountInBuildSettings - 2)
        {
            levelText.text = "credits";
        }
        else
        {
            levelText.text = "level " + (levelSlider.value).ToString();
        }

        qualityText.text = QualitySettings.names[PlayerPrefs.GetInt("Quality")].ToLower();
        qualitySlider.value = PlayerPrefs.GetInt("Quality");

        toggleFps.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("ShowFPS"));

        toggleVSync.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("VSync"));
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MouseSens", sensSlider.value);

        PlayerPrefs.SetFloat("Volume", volumeSlider.value);

        PlayerPrefs.SetInt("Level", (int)levelSlider.value);

        PlayerPrefs.SetInt("Quality", (int)qualitySlider.value);
        QualitySettings.SetQualityLevel((int)qualitySlider.value, true);

        PlayerPrefs.SetInt("ShowFPS", Convert.ToInt32(toggleFps.isOn));

        PlayerPrefs.SetInt("VSync", Convert.ToInt32(toggleVSync.isOn));

        CloseSettings();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void UpdateValues()
    {
        sensText.text = sensSlider.value.ToString("F2");

        volumeText.text = (100 * volumeSlider.value).ToString("F1");

        if (levelSlider.value == 0)
        {
            levelText.text = "tutorial";
        }
        else if (levelSlider.value == SceneManager.sceneCountInBuildSettings - 2)
        {
            levelText.text = "credits";
        }
        else
        {
            levelText.text = "level " + (levelSlider.value).ToString();
        }

        qualityText.text = QualitySettings.names[(int)qualitySlider.value].ToLower();
    }
}
