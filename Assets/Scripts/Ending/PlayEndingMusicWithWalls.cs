using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndingMusicWithWalls : MonoBehaviour
{
    public AudioSource audiomusic;
    void OnDestroy()
    {

        audiomusic.volume = 1;

    }
}
