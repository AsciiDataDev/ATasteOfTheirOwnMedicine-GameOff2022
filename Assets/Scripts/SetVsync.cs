using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetVsync : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = Convert.ToInt32(PlayerPrefs.GetInt("VSync"));
        if (QualitySettings.vSyncCount == 0)
        {
            Application.targetFrameRate = 300;
        }
    }
}
