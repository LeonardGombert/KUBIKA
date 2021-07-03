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

    public void MoveCubeToNode(ref Node targetNode)
    {
        // this happens when the cube is pushed to the edge of the map
        /*if (targetNode == null)
        {
            AssignCarryingCube();
            AssignCarriedByCube();
        }*/

        if ((CubeBehaviors) targetNode.cubeType == CubeBehaviors.None)
        {
            // update nodes' Cube Types
            targetNode.cubeType = cubeBase.cubeType;
            cubeBase.currNode.cubeType = ComplexCubeType.None;

            // Update cube base values
            cubeBase.currNode = targetNode;
            cubeBase.currCoordinates = targetNode.GetNodeCoordinates();

            // Move cube 
            transform.position = targetNode.worldPosition;
        }

        /*AssignCarryingCube();
        AssignCarriedByCube();*/
    }

    // waits for the next frame to reassign carrying/carried cube
    public IEnumerator ReassignCubes()
    {
        yield return null;
        AssignCarryingCube();
        AssignCarriedByCube();
    }

    // check below
    private void AssignCarriedByCube()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1.25f))
        {
            carriedBy = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            if (carriedBy) carriedBy.carrying = this;
        }
        else carriedBy = null;
    }

    // check above
    private void AssignCarryingCube()
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

    private void ResetCurrNode()
    {
        cubeBase.currNode = ReferenceProvider.Instance.KuboGrid.grid[cubeBase.currCoordinates];
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}