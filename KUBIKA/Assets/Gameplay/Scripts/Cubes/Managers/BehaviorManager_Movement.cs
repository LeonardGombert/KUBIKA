using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Movement : MonoBehaviour
    {
        [SerializeField] private Grid_Kubo kuboGrid;
        [SerializeField] BehaviorManager_Carry carryManager;

        public CubeBehavior_Movement movingCube;
        private MoveDirection moveDirection;
        private Node targetNode;
        Vector3Int targetCoordinates;
        
        private List<CubeBehavior_Movement> CarryManagerCubesStack => carryManager.cubesStack;

        public void TryMovingCubeInSwipeDirection(MoveDirection _moveDirection)
        {
            moveDirection = _moveDirection;
            MoveCubes();
        }

        private void MoveCubes()
        {
            carryManager.GetCarriedCubes(ref movingCube);

            for (int i = 0; i < CarryManagerCubesStack.Count; i++)
            {
                targetNode = GetTargetNode(CarryManagerCubesStack[i].cubeBase.currCoordinates);
                if (targetNode != null && HasCubeUnder())
                {
                    // breaking stops any cubes on top from moving
                    if (!CarryManagerCubesStack[i].TryMoveCubeToNode(ref targetNode)) break;
                    CarryManagerCubesStack[i].TryAssignCarryingCube();
                }
            }
        }

        private Node GetTargetNode(Vector3Int cubeBaseCurrCoordinates)
        {
            targetCoordinates = cubeBaseCurrCoordinates;
            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    targetCoordinates += Vector3Int.forward;
                    break;
                case MoveDirection.Right:
                    targetCoordinates += Vector3Int.right;
                    break;
                case MoveDirection.Back:
                    targetCoordinates += Vector3Int.back;
                    break;
                case MoveDirection.Left:
                    targetCoordinates += Vector3Int.left;
                    break;
                case MoveDirection.Down:
                    targetCoordinates += Vector3Int.down;
                    break;
            }

            // check to see if target position is open in the Grid
            kuboGrid.grid.TryGetValue(targetCoordinates, out var tempNode);
            return tempNode;
        }

        private bool HasCubeUnder()
        {
            // check to see if target position has a cube underneath
            kuboGrid.grid.TryGetValue(targetCoordinates + Vector3Int.down, out var tempNode);
            if (tempNode == null) return false;
            return ((CubeBehaviors) tempNode.cubeType) != CubeBehaviors.None;
        }
    }
}