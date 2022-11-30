using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
public class ConsoleCommands : MonoBehaviour
{
    public TMP_InputField commandInput;
    private string[] command;

    void Awake()
    {
        commandInput.Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            try
            {
                command = commandInput.text.Split();
                CommandSelection();
                gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                gameObject.SetActive(false);
            }
        }
        void CommandSelection()
        {
            switch (command[0])
            {
                case "level":
                    LoadLevel();
                    break;
                case "reset":
                    if (command[1] == "level")
                    {
                        resetLevels();
                    }
                    else if (command[1] == "all")
                    {
                        resetAll();
                    }
                    break;
                default:
                    commandInput.text = "";
                    break;
            }
        }
        void LoadLevel()
        {
            SceneManager.LoadScene(int.Parse(command[1]) + 1);
        }
        void resetLevels()
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("UnlockedLevels", 0);
        }
        void resetAll()
        {
            PlayerPrefs.SetFloat("MouseSens", 0.75f);
            PlayerPrefs.SetFloat("Volume", 0.8f);
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("UnlockedLevels", 0);
            PlayerPrefs.SetInt("Quality", 5);
            PlayerPrefs.SetInt("ShowFPS", 0);
            PlayerPrefs.SetInt("VSync", 1);
        }
    }
}
