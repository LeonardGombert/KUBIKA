using System.Collections;
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
        List<CubeBehavior_Movement> CarryManagerCubesStack => carryManager.cubesStack;

        public void TryMovingCubeInSwipeDirection(MoveDirection _moveDirection)
        {
            moveDirection = _moveDirection;
            CheckCarriedPushAndGravity(ref movingCube);
        }

        private void CheckCarriedPushAndGravity(ref CubeBehavior_Movement movingCube)
        {
            carryManager.GetCarriedCubes(ref movingCube);

            for (int i = 0; i < CarryManagerCubesStack.Count; i++)
            {
                targetNode = GetTargetNode(CarryManagerCubesStack[i].cubeBase.currCoordinates);

                if (targetNode != null && bHasCubeUnder())
                {
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    // try pushing cube if there is one
                    if ((targetNode.cubeType & ComplexCubeType.Moveable) != 0)
                    {
                        CarryManagerCubesStack[i].AssignPushingCube(pushManager.GetPushDirection(moveDirection));
                        if (CarryManagerCubesStack[i].pushing != null)
                        {
                            pushManager.ListPushingCubes(ref CarryManagerCubesStack[i].pushing);
                        }

                        for (int j = 0; j < pushManager.pushingCubes.Count; j++)
                        {
                            var pushManagerPushingCube = pushManager.pushingCubes[i];
                            CheckCarriedPushAndGravity(ref pushManagerPushingCube);
                        }
                    }
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    

                    // breaking stops any cubes on top from moving
                    bool movedSuccessfully = CarryManagerCubesStack[i].bMovingCubeToNode(ref targetNode);
                    if (!movedSuccessfully)
                    {
                        // check the current cube in case the cube under it managed to move successfully
                        gravityManager.CheckCubeGravity(CarryManagerCubesStack[i]);
                        break;
                    }

                    CarryManagerCubesStack[i].AssignCarriedByCube();
                    CarryManagerCubesStack[i].AssignCarryingCube();
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
            kuboGrid.grid.TryGetValue(targetCoordinates, out var targetNode);
            return targetNode;
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