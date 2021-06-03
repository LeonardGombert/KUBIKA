using System.Collections;
using System.Collections.Generic;
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
            case MoveDirection.Forward:
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + AbstractGrid.width);
                break;
            case MoveDirection.Right:
                transform.position = new Vector3(transform.position.x + AbstractGrid.width, transform.position.y, transform.position.z);
                break;
            case MoveDirection.Back:
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - AbstractGrid.width);
                break;
            case MoveDirection.Left:
                transform.position = new Vector3(transform.position.x - AbstractGrid.width, transform.position.y, transform.position.z);
                break;
        }
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}