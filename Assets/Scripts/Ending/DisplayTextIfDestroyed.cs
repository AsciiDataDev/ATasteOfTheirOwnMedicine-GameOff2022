using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTextIfDestroyed : MonoBehaviour
{
    public GameObject target;
    public GameObject objectToActivate;
    void Update()
    {
        if(target == null){
            objectToActivate.SetActive(true);
        }
    }
}
