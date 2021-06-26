using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    public CubeBehaviour_Base cubeBase;
    public CubeBehavior_Movement carrying;
    private CubeBehavior_Movement carriedBy; 

    public override void InitBehavior()
    {
        cubeBase = GetComponent<CubeBehaviour_Base>();
    }

    public bool bMovingCubeToNode(ref Node targetNode)
    {
        // the cube is occupied. Cannot move.
        if ((CubeBehaviors)targetNode.cubeType != CubeBehaviors.None)
        {
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
        
        return true;
    }

    // check below
    public void AssignCarriedByCube()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1.25f))
        {
            carriedBy = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            if(carriedBy) carriedBy.carrying = this;
        }
    }

    // check above
    public void AssignCarryingCube()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out var hitInfo, 1.25f))
        {
            carrying = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            if(carrying) carrying.carriedBy = this;
        }
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}