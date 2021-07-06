using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 baseRotation;
    public Transform target;

    private void Start()
    {
        baseRotation = sceneCamera.transform.eulerAngles;
    }

    public void MoveLeft()
    {
        if (sceneCamera.transform.eulerAngles.y < baseRotation.y + 45)
        {
            transform.RotateAround(new Vector3(3.60000014f, 0f, 3.60000014f) / 2, Vector3.up, 360 * Time.deltaTime);
        }
    }

    public void MoveRight()
    {
        if (sceneCamera.transform.eulerAngles.y > baseRotation.y - 45)

        {
            transform.RotateAround(new Vector3(3.60000014f, 0f, 3.60000014f) / 2, Vector3.up, -360 * Time.deltaTime);
        }
    }
}