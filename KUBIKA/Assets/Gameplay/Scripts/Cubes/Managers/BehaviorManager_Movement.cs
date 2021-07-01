using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Movement : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        public Grid_Kubo kuboGrid;
        [SerializeField] private BehaviourManager_PlayerInput playerInput;
        [SerializeField] private BehaviorManager_Carry carryManager;
        [SerializeField] private BehaviorManager_Gravity gravityManager;
        [SerializeField] private BehaviorManager_Push pushManager;

        private MoveDirection moveDirection;
        private Vector3Int targetCoordinates;
        
        public void TryMovingCubeInSwipeDirection(MoveDirection _moveDirection)
        {
            moveDirection = _moveDirection;
            pushManager.pushDirection = _moveDirection;

            Node targetNode = GetTargetNode(playerInput.targetCubeMovement.cubeBase.currCoordinates);

            if (targetNode != null && bHasCubeUnder())
            {
                TryMovingInDirection(ref playerInput.targetCubeMovement);
            }
        }
        
        private void TryMovingInDirection(ref CubeBehavior_Movement movingCube)
        {
            // if there are no cubes in front of you
            if ((CubeBehaviors) GetTargetNode(movingCube.cubeBase.currCoordinates).cubeType == CubeBehaviors.None)
            {
                TryMovingCube(ref movingCube);
            }

            else
            {
                TryPushingCubes(ref movingCube);
            }
        }
        
        private void TryMovingCube(ref CubeBehavior_Movement movingCube)
        {
            Node targetNode = GetTargetNode(movingCube.cubeBase.currCoordinates);
            movingCube.MoveCubeToNode(ref targetNode);
            
            if (carryManager.bAreThereAnyCubesAbove(ref movingCube))
            {
                TryMovingInDirection(ref movingCube.carrying);
            }
        }
        
        // TODO : cubes shouldn't be pushed if they are pushed into nothingness
        private void TryPushingCubes(ref CubeBehavior_Movement movingCube)
        {
            // get cubes to push
            pushManager.ListCubesToPush(ref movingCube);
            
            // if you can't push these cubes, simply apply gravity to yourself in case the cubes below you moved
            if (!pushManager.bCanMovePushingCubes())
            {
                // make cube fall if there is room under
                gravityManager.CheckCubeGravity(movingCube);
                return;
            }

            // if you can push them, then move all of those cubes, followed by yourself, and repeat for cubes that you are carrying
            pushManager.ReversePushingCubesList();

            // push each cube in the list
            for (int i = 0; i < pushManager.cubesToPush.Count; i++)
            {
                var pushedCube = pushManager.cubesToPush[i];
                TryMovingCube(ref pushedCube);
            }

            // clear the list
            pushManager.ClearPushingCubes();
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