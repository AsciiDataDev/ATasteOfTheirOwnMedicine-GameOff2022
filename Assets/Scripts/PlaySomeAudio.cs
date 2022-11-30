using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySomeAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioToPlay;
    void PlayAudio()
    {
        audioToPlay.Play();
    }
}
