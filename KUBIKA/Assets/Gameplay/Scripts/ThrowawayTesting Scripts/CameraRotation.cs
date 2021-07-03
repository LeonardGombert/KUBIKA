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

    void Update()
    {
        if (sceneCamera.transform.eulerAngles.y < baseRotation.y + 60)
        {
            if(Input.GetKey(KeyCode.LeftArrow)) transform.RotateAround(new Vector3(3.60000014f, 0f, 3.60000014f) / 2, Vector3.up, 90 * Time.deltaTime);
        }

        if (sceneCamera.transform.eulerAngles.y > baseRotation.y - 60)
        {
            if(Input.GetKey(KeyCode.RightArrow)) transform.RotateAround(new Vector3(3.60000014f, 0f, 3.60000014f) / 2, Vector3.up, -90 * Time.deltaTime);
        }
    }
}