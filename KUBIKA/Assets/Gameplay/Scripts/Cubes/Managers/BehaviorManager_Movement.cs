using System;
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

        private List<KeyValuePair<CubeBehavior_Movement, Node>> cubesMoved = new List<KeyValuePair<CubeBehavior_Movement, Node>>();
        public Action<List<KeyValuePair<CubeBehavior_Movement, Node>>> doneMovingCubes;
        
        public void TryMovingCubeInSwipeDirection(ref CubeBehavior_Movement targetCube)
        {
            cubesMoved.Clear();
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
                StartCoroutine(TryPushing());
            }
        }

        private void MoveCurrentCube()
        {
            cubesMoved.Add(new KeyValuePair<CubeBehavior_Movement, Node>(currentCube, currentCube.cubeBase.currNode));
            currentCube.MoveCubeToNode(ref destinationNode);
            
            if (currentCube.carrying)
            {
                MakeCarriedCubeFollow();
            }
            else
            {
                // done moving cubes
                doneMovingCubes(cubesMoved);
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

        private IEnumerator TryPushing()
        {
            pushManager.ListOfCubesToPush(ref currentCube);
            
            yield return null;
            
            if (!pushManager.bCanMovePushingCubes())
            {
                MakeBlockedCubesFall();
            }

            else
            {
                PushCubesInList();
            }
        }

        private void MakeBlockedCubesFall()
        {
            gravityManager.CheckCubeGravity(currentCube);
            ReassignCubeStacks();
        }

        private void ReassignCubeStacks()
        {
            // TODO : only reassign from the stack and not all managed cubes (?)
            foreach (var stackedCube in managedCubes)
            {
                StartCoroutine(stackedCube.ReassignCubes());
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