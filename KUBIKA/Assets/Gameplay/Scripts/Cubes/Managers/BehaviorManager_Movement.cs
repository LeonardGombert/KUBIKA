using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Movement : MonoBehaviour
    {
        public Grid_Kubo kuboGrid;
        [SerializeField] BehaviorManager_Carry carryManager;
        [SerializeField] BehaviorManager_Gravity gravityManager;
        [SerializeField] BehaviorManager_Push pushManager;

        public CubeBehavior_Movement movingCube;
        private MoveDirection moveDirection;
        private Node targetNode;
        Vector3Int targetCoordinates;

        private List<CubeBehavior_Movement> CarryManagerCubesStack => carryManager.cubesStack;

        public void TryMovingCubeInSwipeDirection(MoveDirection _moveDirection)
        {
            moveDirection = _moveDirection;
            
            carryManager.GetCarriedCubes(ref movingCube);

            for (int i = 0; i < CarryManagerCubesStack.Count; i++)
            {
                GetTargetNode(CarryManagerCubesStack[i].cubeBase.currCoordinates);

                if (targetNode != null && bHasCubeUnder())
                {
                    // breaking stops any cubes on top from moving
                    if (!CarryManagerCubesStack[i].bMovingCubeToNode(ref targetNode)) break;
                    CarryManagerCubesStack[i].AssignCarriedByCube();
                    CarryManagerCubesStack[i].AssignCarryingCube();
                }
            }
        }
        
        private void GetTargetNode(Vector3Int cubeBaseCurrCoordinates)
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
            kuboGrid.grid.TryGetValue(targetCoordinates, out targetNode);
        }

        private bool bHasCubeUnder()
        {
            // check to see if target position has a cube underneath
            kuboGrid.grid.TryGetValue(targetCoordinates + Vector3Int.down, out var tempNode);
            if (tempNode == null) return false;
            return ((CubeBehaviors) tempNode.cubeType) != CubeBehaviors.None;
        }
    }
}