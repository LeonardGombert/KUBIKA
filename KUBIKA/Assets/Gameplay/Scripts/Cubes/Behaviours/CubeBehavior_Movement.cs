using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    public override void InitBehavior()
    {
    }

    // function overload
    public void PerformBehavior(Vector3 targetPosition)
    {
        // TODO : transform.translate to targetCoords position
        Debug.Log("Moving to " + targetPosition);
        transform.position = targetPosition;
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}