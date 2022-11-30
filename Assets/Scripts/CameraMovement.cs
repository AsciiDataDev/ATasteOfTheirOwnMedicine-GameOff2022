using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensivity;
    [SerializeField] private Transform orientation;
    private float totalRotationX, totalRotationY;
    [HideInInspector] public bool isPaused;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sensivity *= PlayerPrefs.GetFloat("MouseSens");
    }

    void Update()
    {
        if (!isPaused)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        totalRotationY += Input.GetAxisRaw("Mouse X") * sensivity;
        totalRotationX -= Input.GetAxisRaw("Mouse Y") * sensivity;

        totalRotationX = Mathf.Clamp(totalRotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(totalRotationX, totalRotationY, 0);
        orientation.localRotation = Quaternion.Euler(0, totalRotationY, 0);
    }


}
