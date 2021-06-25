using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior
{
    public CubeBehaviour_Base cubeBase;
    public CubeBehavior_Movement carryingCube;

    private CubeBehavior_Movement bottomCube; 

    public override void InitBehavior()
    {
        cubeBase = GetComponent<CubeBehaviour_Base>();
    }

    public bool TryMoveCubeToNode(ref Node targetNode)
    {
        // the cube is occupied. Cannot move
        if (((CubeBehaviors) targetNode.cubeType) != CubeBehaviors.None)
        {
            Debug.Log(gameObject.name + " can't move there.");
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

        Debug.Log(gameObject.name + " moved sucessfully.");
        return true;
    }

    public void TryAssignCarryingCube()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo))
        {
            bottomCube = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            if(bottomCube) bottomCube.carryingCube = this;
        }
    }

    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }
}