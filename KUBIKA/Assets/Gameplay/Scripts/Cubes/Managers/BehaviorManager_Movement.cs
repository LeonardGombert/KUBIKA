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

            // reset list of cubes and size of stack
            cubesThatCanMove.Clear();

            //MoveBaseCubeInDirection();
            StartCheckAtBaseCube(baseCube);

            // finally, move all cubes in the list of cubes that can move
            foreach (var cube in cubesThatCanMove)
            {
                cube.Key.MoveCubeToNode(cube.Value);
            }

            if (baseFallingCube)
            {
                ApplyGravity();
            }
        }


        // start from the base cube. see if that cube has anywhere to go
        // iterate all the way up, check each of the cube's target nodes
        // if one of the cubes has to push another cube, restart the process from the cube you are pushing
        // if one of the cubes has to push a cube that cannot move, stop checking
        // once you've finished checking which cubes can move, apply all movement at the same time
        // if the stack of cubes has changed, reassign carried/carrying cubes. 

        [ShowInInspector] private Dictionary<CubeBehavior_Movement, Node>
            cubesThatCanMove = new Dictionary<CubeBehavior_Movement, Node>();

        [ShowInInspector] private List<CubeBehavior_Movement>
            cubesThatNeedCarryReassignement = new List<CubeBehavior_Movement>();

        private Vector3Int baseCubeDestinationCoords;
        private Node cubeBaseDestinationNode;
        private Vector3Int baseCubeOriginCoords;

        private void StartCheckAtBaseCube(CubeBehavior_Movement baseCube)
        {
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

                    List<CubeBehavior_Movement> ListOfStackedCubes = GetNumberOfStackedCubes(baseCube);

                    for (int i = 0; i < ListOfStackedCubes.Count; i++)
                    {
                        CheckIfCubeCanMove(ListOfStackedCubes[i]);
                    }
                }
            }
        }

        private List<CubeBehavior_Movement> GetNumberOfStackedCubes(CubeBehavior_Movement baseCube)
        {
            List<CubeBehavior_Movement> cubeStackList = new List<CubeBehavior_Movement>();
            CubeBehavior_Movement carriedCube = baseCube;

            while (carriedCube)
            {
                cubeStackList.Add(carriedCube);
                carriedCube = carriedCube.carrying;
            }

            return cubeStackList;
        }

        private void CheckIfCubeCanMove(CubeBehavior_Movement currentCube)
        {
            // get the current destination node, incremented based on how high you are n the stack
            Node currCubeDest = TryGettingBaseCubeTargetNode(currentCube.cubeBase.currCoordinates);

            if (currCubeDest != null)
            {
                // if there is no cube in the way, add the cube to the list of cubes that will move
                if ((CubeBehaviors) currCubeDest.cubeType == CubeBehaviors.None)
                {
                    cubesThatCanMove.Add(currentCube, currCubeDest);
                }

                // otherwise, a cube is in the way -> PUSH or FALL
                else
                {
                    Debug.Log(currentCube.gameObject.name + " is blocked by " + currCubeDest.cubeType);

                    // if the destination cube is of type static, then the cubes can't move anymore
                    if ((currCubeDest.cubeType & ComplexCubeType.Static) != 0)
                    {
                        // add the cube that was doing the carrying to the list of cubes to reassign, 
                        // as it is going to "lose" the cube that it was carrying
                        cubesThatNeedCarryReassignement.Add(currentCube.carriedBy);
                        baseFallingCube = currentCube;
                    }
                    else if ((currCubeDest.cubeType & ComplexCubeType.Moveable) != 0)
                    {
                        // these cubes can be pushed
                        // get the cube at the base
                        // get all cubes above it
                        // try to move the base cube and all cubes above it
                        // if the base cube can move, then so can this one

                        CubeBehavior_Movement pushingCube =
                            currCubeDest.cubeAtPosition.GetComponent<CubeBehavior_Movement>();

                        List<CubeBehavior_Movement> ListOfStackedCubes = GetNumberOfStackedCubes(pushingCube);

                        for (int i = 0; i < ListOfStackedCubes.Count; i++)
                        {
                            CheckIfCubeCanMove(ListOfStackedCubes[i]);
                        }

                        if (cubesThatCanMove.ContainsKey(pushingCube))
                        {
                            cubesThatCanMove.Add(currentCube, currCubeDest);
                        }
                    }
                }
            }
        }

        private CubeBehavior_Movement baseFallingCube;

        private void ApplyGravity()
        {
            // make this cube fall
            MakeCubeFall(baseFallingCube);

            foreach (var fallenCube in cubesThatNeedCarryReassignement)
            {
                fallenCube.Reassign();
            }

            cubesThatNeedCarryReassignement.Clear();
            baseFallingCube = null;
        }

        private void MakeCubeFall(CubeBehavior_Movement currentCube)
        {
            Node targetNode = Node.Zero;
            int iterations = 1;

            // get the node right under
            kuboGrid.grid.TryGetValue(currentCube.cubeBase.currCoordinates + (Vector3Int.down * iterations),
                out Node tmp);

            // while the node under is free, keep falling
            do
            {
                targetNode = tmp;
                kuboGrid.grid.TryGetValue(currentCube.cubeBase.currCoordinates + (Vector3Int.down * ++iterations),
                    out tmp);
                // TODO : falling out of the kubo will undoubtedly break the game
            } while ((CubeBehaviors) tmp.cubeType == CubeBehaviors.None);

            currentCube.MoveCubeToNode(targetNode);
            cubesThatNeedCarryReassignement.Add(currentCube);

            if (currentCube.carrying)
            {
                RecursivelyMakeCarriedCubesFall(currentCube.carrying);
            }
        }

        private void RecursivelyMakeCarriedCubesFall(CubeBehavior_Movement currentCube)
        {
            kuboGrid.grid.TryGetValue(currentCube.carriedBy.cubeBase.currCoordinates + (Vector3Int.up),
                out Node targetNode);

            currentCube.MoveCubeToNode(targetNode);

            cubesThatNeedCarryReassignement.Add(currentCube);

            if (currentCube.carrying)
            {
                RecursivelyMakeCarriedCubesFall(currentCube.carrying);
            }
        }

        private Node GetCurrCubeDestinationNode(int stackedCubeIndex)
        {
            return kuboGrid.grid[baseCubeDestinationCoords + (Vector3Int.up * stackedCubeIndex)];
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