using System.Collections;
using System.Collections.Generic;
using Gameplay.Scripts.Cubes.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehavior_Movement : AbstractCubeBehavior, IUndoable
{
    public CubeBehaviour_Base cubeBase;
    [ReadOnly] public CubeBehavior_Movement carrying;
    [ReadOnly] public CubeBehavior_Movement carriedBy;

    private UndoManager undoManager;

    private Stack<Node> previousPositions = new Stack<Node>();

    public override void InitBehavior()
    {
        AssignCarriedByCube();
        AssignCarryingCube();
        ResetCurrNode();

        undoManager = FindObjectOfType<UndoManager>();
    }

    public void MoveCubeToNode(Node targetNode)
    {
        previousPositions.Push(cubeBase.currNode);
        undoManager.RegisterOne(this);

        // update nodes' Cube Types
        targetNode.cubeType = cubeBase.cubeType;
        cubeBase.currNode.cubeType = ComplexCubeType.None;

        // Update cube base values
        cubeBase.currNode = targetNode;
        cubeBase.currCoordinates = targetNode.GetNodeCoordinates();

        // Move cube 
        transform.position = targetNode.worldPosition;

        // StartCoroutine(ReassignCubes());
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

    public override void UndoLast()
    {
        var temp = previousPositions.Pop();
        MoveCubeToNode(temp);
    }
}