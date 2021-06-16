using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    private CubeBehaviour_Base cubeBase;
    public TriCoords myTriCoords;
    
    public override void InitBehavior()
    {
        cubeBase = GetComponent<CubeBehaviour_Base>();
        myTriCoords = cubeBase.TriCoords;
    }

    public void PerformBehavior(MoveDirection moveDirection)
    {
        switch (moveDirection)
        {
            case MoveDirection.None:
                break;
            case MoveDirection.Forward:
                transform.position += Vector3.forward * AbstractGrid.width;
                myTriCoords += TriCoords.Forward;
                break;
            case MoveDirection.Right:
                transform.position += Vector3.right * AbstractGrid.width;
                myTriCoords += TriCoords.Right;
                break;
            case MoveDirection.Back:
                transform.position += Vector3.back * AbstractGrid.width;
                myTriCoords += TriCoords.Back;
                break;
            case MoveDirection.Left:
                transform.position += Vector3.left * AbstractGrid.width;
                myTriCoords += TriCoords.Left;
                break;
            case MoveDirection.Down:
                transform.position += Vector3.down * AbstractGrid.width;
                myTriCoords += TriCoords.Down;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }
        cubeBase.TriCoords = myTriCoords;
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}