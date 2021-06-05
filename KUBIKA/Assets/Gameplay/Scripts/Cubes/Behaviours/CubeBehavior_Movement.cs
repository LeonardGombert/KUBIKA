using System;
using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    public override void InitBehavior()
    {
        throw new System.NotImplementedException();
    }

    public void PerformBehavior(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.None:
                break;
            case MoveDirection.Forward:
                transform.position += Vector3.forward * AbstractGrid.width;
                break;
            case MoveDirection.Right:
                transform.position += Vector3.right * AbstractGrid.width;
                break;
            case MoveDirection.Back:
                transform.position += Vector3.back * AbstractGrid.width;
                break;
            case MoveDirection.Left:
                transform.position += Vector3.left * AbstractGrid.width;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}