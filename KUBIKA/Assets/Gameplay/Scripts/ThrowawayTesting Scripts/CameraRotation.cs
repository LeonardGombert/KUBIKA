using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Vector3 rotation;

    private void Awake()
    {
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.y += Time.deltaTime * 25f;
        transform.eulerAngles = rotation;
    }
}
