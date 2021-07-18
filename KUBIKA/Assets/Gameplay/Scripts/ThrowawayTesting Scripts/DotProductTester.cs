using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProductTester : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    
    // Update is called once per frame
    void Update()
    {
        var dotProduct = Vector3.Dot(object1.position.normalized, object2.position.normalized);
        // Debug.Log(dotProduct);
        // Debug.DrawLine(transform.position, object1.position, Color.green);
        // Debug.DrawLine(transform.position, object2.position, Color.red);
    }
}
