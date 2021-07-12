using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.ShaderGraph.Internal;
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

        private CubeBehavior_Movement baseCube;
        private Node destinationNode;

        public void TryMovingCubeInSwipeDirection(CubeBehavior_Movement targetCube)
        {
            moveDirection = playerInput.CalculateMoveDirection();
            baseCube = targetCube;

            // see about this later//
            pushManager.pushDirection = moveDirection;

            //MoveBaseCubeInDirection();
            StartCheckAtBaseCube();
        }


        // start from the base cube. see if that cube has anywhere to go
        // iterate all the way up, check each of the cube's target nodes
        // if one of the cubes has to push another cube, restart the process from the cube you are pushing
        // if one of the cubes has to push a cube that cannot move, stop checking
        // once you've finished checking which cubes can move, apply all movement at the same time
        // if the stack of cubes has changed, reassign carried/carrying cubes. 

        [ShowInInspector]
        private Dictionary<CubeBehavior_Movement, Node>
            cubesThatCanMove = new Dictionary<CubeBehavior_Movement, Node>();

        private Vector3Int baseCubeDestinationCoords;
        private Node cubeBaseDestinationNode;
        private Vector3Int baseCubeOriginCoords;

        private void StartCheckAtBaseCube()
        {
            // reset list of cubes and size of stack
            currentCubeInStack = 0;
            cubesThatCanMove.Clear();
            
            // get/set the target node of the base cube
            baseCubeOriginCoords = baseCube.cubeBase.currCoordinates;
            cubeBaseDestinationNode = TryGettingBaseCubeTargetNode(baseCubeOriginCoords);

            // if the node exists
            if (cubeBaseDestinationNode != null)
            {
                // check if the current cube has a node under it, and a node under the target
                if (OriginHasCubeUnder(baseCubeOriginCoords) && TargetHasCubeUnder())
                {
                    // if it does, get the coordinates of the target node and recursively check if each cube in the stack can move
                    baseCubeDestinationCoords = cubeBaseDestinationNode.GetNodeCoordinates();
                    RecursivelyCheckIfCubesCanMove(baseCube);
                }
            
                // finally, move all cubes in the list of cubes that can move
                foreach (var cube in cubesThatCanMove)
                {
                    cube.Key.MoveCubeToNode(cube.Value);
                }
            }
        }

        private void RecursivelyCheckIfCubesCanMove(CubeBehavior_Movement currentCube)
        {
            // get the current destination node, incremented based on how high you are n the stack
            Node currCubeDest = GetCurrCubeDestinationNode();
            
            if (currCubeDest != null)
            {
                // if there is no cube in the way, add the cube to the list of cubes that will move
                if ((CubeBehaviors) currCubeDest.cubeType == CubeBehaviors.None)
                {
                    cubesThatCanMove.Add(currentCube, currCubeDest);
                }
                else
                {
                    // otherwise, you'll need to try and push the cube in the way
                }
            
                if (currentCube.carrying)
                {
                    RecursivelyCheckIfCubesCanMove(currentCube.carrying);
                }
            }

            Debug.Log("Finished Recursive Loop");
        }
        
        private Node GetCurrCubeDestinationNode()
        {
            return kuboGrid.grid[baseCubeDestinationCoords + (Vector3Int.up * currentCubeInStack++)];
        }


        #region MyRegion

        #region Moving

        private void MoveBaseCubeInDirection()
        {
            Vector3Int coords = baseCube.cubeBase.currCoordinates;
            destinationNode = TryGettingBaseCubeTargetNode(coords);
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
            baseCube.MoveCubeToNode(destinationNode);

            if (baseCube.carrying)
            {
                MakeCarriedCubeFollow();
            }
            else
            {
                foreach (var cubeBehavior in managedCubes)
                {
                    StartCoroutine(cubeBehavior.ReassignCubes());
                }
            }
        }

        private void MakeCarriedCubeFollow()
        {
            baseCube = baseCube.carrying;
            destinationNode = GetCarriedCubeTargetNode();
            TryMovingCurrentCube();
        }

        #endregion

        #region Pushing

        private IEnumerator TryPushing()
        {
            pushManager.ListOfCubesToPush(baseCube);

            yield return null;

            if (!pushManager.bCanMovePushingCubes())
            {
                gravityManager.CheckCubeGravity(baseCube);
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
                baseCube = pushManager.cubesToPush[i];
                MoveBaseCubeInDirection();
            }

            pushManager.ClearPushingCubes();
        }

        #endregion

        #region Helper Functions

        private Node TryGettingBaseCubeTargetNode(Vector3Int cubeBaseCurrCoordinates)
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

        #endregion
    }
}