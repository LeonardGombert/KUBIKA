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
        Vector3Int targetCoordinates;
        List<CubeBehavior_Movement> CubeStack => carryManager.cubesStack;
        [SerializeField] List<CubeBehavior_Movement> movingCubes = new List<CubeBehavior_Movement>();

        public void TryMovingCubeInSwipeDirection(MoveDirection _moveDirection)
        {
            moveDirection = _moveDirection;
            pushManager.pushDirection = _moveDirection;
            movingCubes.Clear();
            
            // Check to see if there are cubes in the way
            AreThereCubesInTheWayOf(ref movingCube);
        }

        private void AreThereCubesInTheWayOf(ref CubeBehavior_Movement movingCube)
        {
            // does the target Node exist, and is there a cube I can move onto ?
            Node targetNode = GetMovingCubeTargetNode(movingCube.cubeBase.currCoordinates);
                if (targetNode != null  && bHasCubeUnder()) 
                {
                    // are there cubes in the way of the moving cube ? 
                    
                    // no -> call the move function and repeat for cubes that you are carrying
                    if ((CubeBehaviors)targetNode.cubeType == CubeBehaviors.None)
                    {
                        MoveTheCurrentCube(ref movingCube);
                        AreThereAnyCubesAboveYou(ref movingCube);
                    }

                    // otherwise, get a list of the cubes you are trying to push
                    else
                    {
                        pushManager.GetPushingCubes(ref movingCube);
                    }
                    
                    // if you can't push these cubes, simply apply gravity to yourself
                    if (!pushManager.bCanMovePushingCubes())
                    {
                        gravityManager.CheckCubeGravity(movingCube);
                    }
                    
                    // if you can push them, then move all of those cubes, followed by yourself, and repeat for cubes that you are carrying
                    else if (pushManager.bCanMovePushingCubes())
                    {
                        pushManager.cubesToPush.Reverse();
                        
                        for (int i = 0; i < pushManager.cubesToPush.Count; i++)
                        {
                            Debug.Log(i);
                            var cubeBehaviorMovement = pushManager.cubesToPush[i];
                            MoveTheCurrentCube(ref cubeBehaviorMovement);
                            AreThereAnyCubesAboveYou(ref cubeBehaviorMovement);
                        }
                        pushManager.ClearPushingCubes();
                    }
                }
        }

        private void MoveTheCurrentCube(ref CubeBehavior_Movement movingCube)
        {
            Node targetNode = GetMovingCubeTargetNode(movingCube.cubeBase.currCoordinates);
            movingCube.bMovingCubeToNode(ref targetNode);
        }

        private void AreThereAnyCubesAboveYou(ref CubeBehavior_Movement theMovingCube)
        {
            // is there a carriedCube in the moving cube ? 
                // no -> do nothing
                
                // yes -> then call AreThereCubesInTheWayOf(carriedCube)
                if (theMovingCube.carrying != null)
                {
                    AreThereCubesInTheWayOf(ref theMovingCube.carrying);
                }
        }

        private Node GetMovingCubeTargetNode(Vector3Int cubeBaseCurrCoordinates)
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