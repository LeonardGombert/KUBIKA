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
                TryMovingCube(ref playerInput.targetCubeMovement);
            }
        }

        private void TryMovingCube(ref CubeBehavior_Movement movingCube)
        {
            TryMovingInDirection(ref movingCube);
            TryPushingCubes(ref movingCube);
        }

        private void TryMovingInDirection(ref CubeBehavior_Movement movingCube)
        {
            Node targetNode = GetTargetNode(movingCube.cubeBase.currCoordinates);

            if ((CubeBehaviors) targetNode.cubeType == CubeBehaviors.None)
            {
                MoveCurrentCube(ref movingCube);
                MoveCarriedCubes(ref movingCube);
            }

            // otherwise, get a list of the cubes you are trying to push
            else
            {
                pushManager.ListCubesToPush(ref movingCube);
            }
        }
        
        private void MoveCurrentCube(ref CubeBehavior_Movement movingCube)
        {
            Node targetNode = GetTargetNode(movingCube.cubeBase.currCoordinates);
            movingCube.MoveCubeToNode(ref targetNode);
        }

        private void MoveCarriedCubes(ref CubeBehavior_Movement movingCube)
        {
            if (carryManager.bAreThereAnyCubesAbove(ref movingCube))
            {
                TryMovingCube(ref movingCube.carrying);
            }
        }
        
        private void TryPushingCubes(ref CubeBehavior_Movement movingCube)
        {
            // if you can't push these cubes, simply apply gravity to yourself
            if (!pushManager.bCanMovePushingCubes())
            {
                gravityManager.CheckCubeGravity(movingCube);
                return;
            }

            PushCubes();
        }

        private void PushCubes()
        {
            // if you can push them, then move all of those cubes, followed by yourself, and repeat for cubes that you are carrying
            pushManager.ReversePushingCubesList();

            for (int i = 0; i < pushManager.cubesToPush.Count; i++)
            {
                var pushedCube = pushManager.cubesToPush[i];
                MoveCurrentCube(ref pushedCube);
                MoveCarriedCubes(ref pushedCube);
            }

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