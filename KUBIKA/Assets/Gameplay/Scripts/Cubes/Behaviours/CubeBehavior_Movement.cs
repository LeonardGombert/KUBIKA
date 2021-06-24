using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    private CubeBehaviour_Base cubeBase;
    
    public override void InitBehavior()
    {
        cubeBase = GetComponent<CubeBehaviour_Base>();
    }

    // function overload
    public void PerformBehavior(ref Node targetNode)
    {
        targetNode.cubeType = cubeBase.cubeType;
        
        cubeBase.currNode = targetNode;
        cubeBase.currCoordinates = targetNode.GetNodeCoordinates();
        
        transform.position = targetNode.worldPosition;
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}