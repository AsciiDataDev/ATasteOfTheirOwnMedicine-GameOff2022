using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject prefabPlayer;
    void Awake()
    {
        Instantiate(prefabPlayer, transform.position,Quaternion.LookRotation(-transform.up));
    }
}
