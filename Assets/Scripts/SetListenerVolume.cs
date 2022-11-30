using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetListenerVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    void Awake()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }

    public void ChangeVolume()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }
}
