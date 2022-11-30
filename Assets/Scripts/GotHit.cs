using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotHit : MonoBehaviour
{
    HealthManager healthManager;
    void Start()
    {
        healthManager = GameObject.FindObjectOfType<HealthManager>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            healthManager.DamageToPlayer(100);
        }
    }
}
