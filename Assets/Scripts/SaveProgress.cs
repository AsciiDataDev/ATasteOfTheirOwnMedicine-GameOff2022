using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveProgress : MonoBehaviour
{
    void Start()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        PlayerPrefs.SetInt("Level", thisSceneIndex);
        if (PlayerPrefs.GetInt("UnlockedLevels") < thisSceneIndex)
        {
            PlayerPrefs.SetInt("UnlockedLevels", thisSceneIndex);
        }

    }
}
