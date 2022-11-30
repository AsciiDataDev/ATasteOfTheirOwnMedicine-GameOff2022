using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDelayToWakeUp : MonoBehaviour
{
    public float delayToWakeUp;
    private WakeUpAndGo wakeUpAndGo;
    [SerializeField] private bool haveTrigger;
    void Awake()
    {
        if (!haveTrigger)
        {
            SetScriptsActive();
        }
    }

    public void SetScriptsActive()
    {
        wakeUpAndGo = GetComponentInChildren<WakeUpAndGo>();
        wakeUpAndGo.delayToWakeUp = delayToWakeUp;
        wakeUpAndGo.SetWakingUp();
    }
}
