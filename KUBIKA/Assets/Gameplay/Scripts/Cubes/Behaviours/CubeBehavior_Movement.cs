using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    public CubeBehaviour_Base cubeBase;
    [ReadOnly] public CubeBehavior_Movement carrying;
    [ReadOnly] public CubeBehavior_Movement carriedBy;
    [ReadOnly] public CubeBehavior_Movement pushing;
    [ReadOnly] public CubeBehavior_Movement pushedBy;

    public override void InitBehavior()
    {
        AssignCarriedByCube();
        AssignCarryingCube();
        ResetCurrNode();
    }

    public bool bMovingCubeToNode(ref Node targetNode)
    {
        if (targetNode == null)
        {
            // this happens when the cube is pushed to the edge of the map
            StartCoroutine(Reassign());
            return false;
        }
        
        // the cube is occupied. Cannot move.
        if ((CubeBehaviors) targetNode.cubeType != CubeBehaviors.None)
        {
            StartCoroutine(Reassign());
            return false;
        }
        
        // update nodes' Cube Types
        targetNode.cubeType = cubeBase.cubeType;
        cubeBase.currNode.cubeType = ComplexCubeType.None;

        // Update cube base values
        cubeBase.currNode = targetNode;
        cubeBase.currCoordinates = targetNode.GetNodeCoordinates();

        // Move cube 
        transform.position = targetNode.worldPosition;

        StartCoroutine(Reassign());
        
        return true;
    }

    private IEnumerator Reassign()
    {
        yield return null;
        AssignCarryingCube();
        AssignCarriedByCube();
    }

    // check below
    public void AssignCarriedByCube()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1.25f))
        {
            carriedBy = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            if (carriedBy) carriedBy.carrying = this;
        }
        else carriedBy = null;
    }

    // check above
    public void AssignCarryingCube()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out var hitInfo, 1.25f))
        {
            carrying = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            if (carrying) carrying.carriedBy = this;
        }
        else
        {
            carrying = null;
        }
    }

    public void AssignPushingCube(Vector3 moveDirection)
    {
        if (Physics.Raycast(transform.position, moveDirection, out var hitInfo, 1.25f))
        {
            pushing = hitInfo.collider.GetComponent<CubeBehavior_Movement>();

            if (pushing)
            {
                pushing.AssignPushingCube(moveDirection);
            }
        }
    }

    private void ResetCurrNode()
    {
        cubeBase.currNode = ReferenceProvider.Instance.KuboGrid.grid[cubeBase.currCoordinates];
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}