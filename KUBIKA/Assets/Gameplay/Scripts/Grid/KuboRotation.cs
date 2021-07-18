using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class KuboRotation : MonoBehaviour
{
    [ShowInInspector, ReadOnly] public static KuboState State;
    [SerializeField] private KuboGravityApplicator gravityApplicator;

    public void RotateRight()
    {
        if (State == KuboState.Position3)
        {
            State = KuboState.Position1;
        }
        else
        {
            State += 1;
        }

        ApplyRotation();
    }

    public void RotateLeft()
    {
        if (State == KuboState.Position1)
        {
            State = KuboState.Position3;
        }
        else
        {
            State -= 1;
        }

        ApplyRotation();
    }

    private void ApplyRotation()
    {
        switch (State)
        {
            case KuboState.Position1:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case KuboState.Position2:
                transform.eulerAngles = new Vector3(0, 90, 90);
                break;
            case KuboState.Position3:
                transform.eulerAngles = new Vector3(270, 270, 0);
                break;
        }

        gravityApplicator.MakeFallAfterRotation();
    }
}