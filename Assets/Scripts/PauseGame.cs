using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject PauseCanvas;
    public bool isPaused;
    public bool canPause;
    private AudioSource[] audioSources;
    void Start()
    {
        PauseCanvas.SetActive(false);
        canPause = true;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && canPause)
        {
                TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        PauseCanvas.SetActive(isPaused);

        GameObject.FindObjectOfType<CameraMovement>().isPaused = isPaused;
        GameObject.FindObjectOfType<TimeManager>().isPaused = isPaused;
        GameObject.FindObjectOfType<HoldAndShoot>().isPaused = isPaused;

        Cursor.visible = isPaused;

        if (isPaused)
        {
            audioSources = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource audios in audioSources)
            {
                audios.Pause();
            }
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            foreach (AudioSource audios in audioSources)
            {
                audios.UnPause();
            }
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
