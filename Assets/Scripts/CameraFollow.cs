using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraPositioner;
    void Update()
    {
        transform.position = cameraPositioner.position;
    }
}
