using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private GameObject GameoverCanvas;
    public float playeMaxHealth;
    private float playerCurrentHealth;
    [HideInInspector] public bool gameOver;
    private SceneChanger sceneChanger;
    void Start()
    {
        GameoverCanvas.SetActive(false);
        playerCurrentHealth = playeMaxHealth;
        sceneChanger = GameObject.FindObjectOfType<SceneChanger>();
    }

    public void DamageToPlayer(float damageQuantity)
    {
        playerCurrentHealth -= damageQuantity;
        if (playerCurrentHealth <= 0)
        {
            DoGameOver();
        }
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            sceneChanger.RestartGame();
        }
    }

    public void DoGameOver()
    {
        GameObject.FindObjectOfType<PauseGame>().canPause = false;

        Destroy(GameObject.FindObjectOfType<TimeManager>());

        GameoverCanvas.SetActive(true);

        Time.timeScale = 0;

        AudioSource[] audioSources;
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource audios in audioSources)
        {
            if (audios.tag == "Unaffected")
            {
                audios.pitch = 1;
            }
            else
            {
                audios.volume = 0;
            }
        }

        gameOverSound.Play();

        gameOver = true;
    }
}
