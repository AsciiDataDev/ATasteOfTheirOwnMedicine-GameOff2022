using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class TimeManager : MonoBehaviour
{
    public PostProcessVolume volume;
    public AudioSource timeContinueAudio, timeStopAudio;
    public TextMeshProUGUI timerText;
    public float abilityDelay = 0;
    public float abilityDuration = 0;
    public float slowFactor = 0.05f;
    AudioSource[] audioSources;
    public bool isSlowed = false;
    private bool canSlow = true;
    private float timeLeft;
    [HideInInspector] public bool isPaused;

    private ChromaticAberration chromaticAberration;

    private bool timerForDelayOn, timerForDurationOn;
    void Start()
    {
        volume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isPaused)
        {
            if (isSlowed)
            {
                EndSlowMotion();
            }
            else if (canSlow)
            {
                DoSlowMotion();
            }
        }

        updateTimer();
    }

    void updateTimer()
    {
        if (timeLeft > 0 && !isPaused)
        {
            timeLeft -= Time.unscaledDeltaTime;
            if (timeLeft <= 0)
            {
                if (timerForDelayOn)
                {
                    canSlow = true;
                    timerForDelayOn = false;
                }
                else if (timerForDurationOn)
                {
                    timerForDurationOn = false;
                    EndSlowMotion();
                }
                timerText.text = "";
                return;
            }
            timerText.text = timeLeft.ToString("F2");
        }
    }

    public void EndSlowMotion()
    {
        Time.timeScale = 1;
        audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audios in audioSources)
        {
            if (audios.tag != "Unscaled" && audios.tag != "Unaffected")
            {
                audios.pitch = 1;
            }
        }
        isSlowed = false;
        canSlow = false;
        timerForDelayOn = true;
        timerText.color = Color.black;
        timeContinueAudio.Play();
        chromaticAberration.active = false;

        StartTimer(abilityDelay);
    }
    public void DoSlowMotion()
    {
        Time.timeScale = slowFactor;
        audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audios in audioSources)
        {
            if (audios.tag != "Unscaled")
            {
                audios.pitch = 0.35f;
            }
        }
        isSlowed = true;
        timerForDurationOn = true;
        timerText.color = Color.white;
        timeStopAudio.Play();
        chromaticAberration.active = true;

        StartTimer(abilityDuration);
    }

    void StartTimer(float time)
    {
        timeLeft = time;
    }

}
