using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Movement : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        public Grid_Kubo kuboGrid;
        [SerializeField] private BehaviourManager_PlayerInput playerInput;
        [SerializeField] private BehaviorManager_Gravity gravityManager;
        [SerializeField] private BehaviorManager_Push pushManager;

        private MoveDirection moveDirection;
        private Vector3Int targetCoordinates;
        private int currentCubeInStack;

        private CubeBehavior_Movement currentCube;
        private Node destinationNode;

        public void TryMovingCubeInSwipeDirection(ref CubeBehavior_Movement targetCube)
        {
            moveDirection = playerInput.CalculateMoveDirection();
            currentCube = targetCube;

            // see about this later//
            pushManager.pushDirection = moveDirection;

            MoveBaseCubeInDirection();
        }

        #region Moving

        private void MoveBaseCubeInDirection()
        {
            Vector3Int coords = currentCube.cubeBase.currCoordinates;
            destinationNode = TryGettingBaseCubeNode(coords);
            currentCubeInStack = 0;

            if (destinationNode == null) return;

            if (OriginHasCubeUnder(coords) && TargetHasCubeUnder())
            {
                TryMovingCurrentCube();
            }
        }

        private void TryMovingCurrentCube()
        {
            if ((CubeBehaviors) destinationNode.cubeType == CubeBehaviors.None)
            {
                MoveCurrentCube();
            }

            else
            {
                TryPushing();
            }
        }

        private void MoveCurrentCube()
        {
            currentCube.MoveCubeToNode(ref destinationNode);

            if (currentCube.carrying)
            {
                MakeCarriedCubeFollow();
            }
        }

        private void MakeCarriedCubeFollow()
        {
            currentCube = currentCube.carrying;
            destinationNode = GetCarriedCubeTargetNode();
            TryMovingCurrentCube();
        }

        #endregion

        #region Pushing

        private void TryPushing()
        {
            pushManager.ListOfCubesToPush(ref currentCube);

            if (!pushManager.bCanMovePushingCubes())
            {
                gravityManager.CheckCubeGravity(currentCube);
            }

            else
            {
                PushCubesInList();
            }
        }

        private void PushCubesInList()
        {
            pushManager.ReversePushingCubesList();

            for (int i = 0; i < pushManager.cubesToPush.Count; i++)
            {
                currentCube = pushManager.cubesToPush[i];
                MoveBaseCubeInDirection();
            }

            pushManager.ClearPushingCubes();
        }

        #endregion

        #region Helper Functions

        private Node TryGettingBaseCubeNode(Vector3Int cubeBaseCurrCoordinates)
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

        private Node GetCarriedCubeTargetNode()
        {
            return kuboGrid.grid[targetCoordinates + (Vector3Int.up * ++currentCubeInStack)];
        }

        private bool OriginHasCubeUnder(Vector3Int cubeBaseCurrCoordinates)
        {
            // check to see if current cube has a cube underneath
            kuboGrid.grid.TryGetValue(cubeBaseCurrCoordinates + Vector3Int.down, out var tempNode);
            if (tempNode == null) return false;
            return ((CubeBehaviors) tempNode.cubeType) != CubeBehaviors.None;
        }

        private bool TargetHasCubeUnder()
        {
            // check to see if target position has a cube underneath
            kuboGrid.grid.TryGetValue(targetCoordinates + Vector3Int.down, out var tempNode);
            if (tempNode == null) return false;
            return ((CubeBehaviors) tempNode.cubeType) != CubeBehaviors.None;
        }

        #endregion
    }
}