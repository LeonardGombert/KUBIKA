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
        transform.RotateAround(new Vector3(3.60000014f, 0f, 3.60000014f) / 2, Vector3.up, 90 * Time.deltaTime);
    }

    public void MoveRight()
    {
        transform.RotateAround(new Vector3(3.60000014f, 0f, 3.60000014f) / 2, Vector3.up, -90 * Time.deltaTime);
    }
}