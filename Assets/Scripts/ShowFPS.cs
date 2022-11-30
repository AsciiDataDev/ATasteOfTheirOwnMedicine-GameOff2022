using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ShowFPS : MonoBehaviour
{
    public TextMeshProUGUI text;
    float deltaTime;
    void Awake()
    {
        if (!Convert.ToBoolean(PlayerPrefs.GetInt("ShowFPS")))
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        text.text = Mathf.Ceil(fps).ToString();
    }
}
