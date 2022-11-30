using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public SetDelayToWakeUp[] setDelayToWakeUp;
    void OnTriggerEnter(Collider col)
    {
        foreach (SetDelayToWakeUp x in setDelayToWakeUp)
        {
            x.SetScriptsActive();
        }
        Destroy(gameObject);
    }
}
