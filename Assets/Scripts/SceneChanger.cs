using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void NextSceneIndex(int currentIndex)
    {
        SceneManager.LoadScene(currentIndex + 1);
        Time.timeScale = 1;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        if (sceneName == "Menu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    public void StartSceneFromMenu()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 1);
    }
}
