using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Movement : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        public Grid_Kubo kuboGrid;
        [SerializeField] private BehaviourManager_PlayerInput playerInput;
        [SerializeField] UndoManager undoManager;

        [SerializeField, ReadOnly] private MoveDirection moveDirection;
        private Vector3Int targetCoordinates;
        private CubeBehavior_Movement playerMovedCube;
        private CubeBehavior_Movement baseFallingCube;

        private readonly Dictionary<CubeBehavior_Movement, Node> cubesTaggedForMovement =
            new Dictionary<CubeBehavior_Movement, Node>();

        private readonly List<CubeBehavior_Movement> cubesThatNeedCarryReassignement =
            new List<CubeBehavior_Movement>();

        public void TryMovingCubeInSwipeDirection(CubeBehavior_Movement targetCube)
        {
            moveDirection = playerInput.CalculateMoveDirection();
            playerMovedCube = targetCube;

            if (CheckIfCanMoveBaseCube())
            {
                TagCubesToMove();

                MoveTaggedCubes();

                ApplyGravityToBlockedCubes();
            }
        }

        #region MOVEMENT

        private bool CheckIfCanMoveBaseCube()
        {
            // get the start and end coordinates for the cube the player is moving
            var startCoords = playerMovedCube.cubeBase.currCoordinates;
            var targetNode = GetMovingCubeTargetNode(startCoords);

            // if the node exists
            if (targetNode != null && targetNode.cubeType != ComplexCubeType.Static)
            {
                var targetCoords = targetNode.GetNodeCoordinates();

                // check if the current cube has a node under it, and a node under the target
                if (HasCubeUnderOrigin(startCoords) && HasCubeUnderTarget(targetCoords))
                {
                    return true;
                }
            }

            return false;
        }

        private void TagCubesToMove()
        {
            var stackedCubesList = GetStackedCubes(playerMovedCube);

            foreach (var cube in stackedCubesList)
            {
                if (!CheckIfCubeCanMove(cube))
                {
                    break;
                }
            }
        }

        private List<CubeBehavior_Movement> GetStackedCubes(CubeBehavior_Movement stackBaseCube)
        {
            var cubeStackList = new List<CubeBehavior_Movement>();
            var currentCube = stackBaseCube;

            while (currentCube)
            {
                cubeStackList.Add(currentCube);
                currentCube = currentCube.carrying;
            }

            return cubeStackList;
        }

        private bool CheckIfCubeCanMove(CubeBehavior_Movement movingCube)
        {
            // get the current destination node, incremented based on how high you are n the stack
            var targetNode = GetMovingCubeTargetNode(movingCube.cubeBase.currCoordinates);

            if (targetNode != null && HasCubeUnderOrigin(targetNode.GetNodeCoordinates()))
            {
                // if there is no cube in the way, add the cube to the list of cubes that will move
                if ((CubeBehaviors) targetNode.cubeType == CubeBehaviors.None)
                {
                    cubesTaggedForMovement.Add(movingCube, targetNode);
                    targetNode.cubeType = movingCube.cubeBase.cubeType;
                    return true;
                }

                // if the destination cube is of type static, then the cubes can't move anymore
                if ((targetNode.cubeType & ComplexCubeType.Static) != 0)
                {
                    cubesThatNeedCarryReassignement.Add(movingCube.carriedBy);
                    baseFallingCube = movingCube;
                    return false;
                }

                if ((targetNode.cubeType & ComplexCubeType.Moveable) != 0)
                {
                    CubeBehavior_Movement pushingCube =
                        targetNode.cubeAtPosition.GetComponent<CubeBehavior_Movement>();

                    if (CheckIfCubeCanMove(pushingCube)) // the pushed cube is tagged in this loop
                    {
                        cubesTaggedForMovement.Add(movingCube, targetNode);
                        targetNode.cubeType = movingCube.cubeBase.cubeType;

                        if (pushingCube.carrying)
                        {
                            var stackedCubesBeingPushed = GetStackedCubes(pushingCube.carrying);

                            foreach (var pushedCube in stackedCubesBeingPushed)
                            {
                                if (!CheckIfCubeCanMove(pushedCube))
                                {
                                    break;
                                }
                            }

                            // once you've gotten through all of the stacked cubes you are pushing,
                            // //move them to make room for the (potentially) stacked cubes that
                            // initiated the push
                            MoveTaggedCubes();
                        }

                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        private void MoveTaggedCubes()
        {
            undoManager.RegisterOneMove(cubesTaggedForMovement);

            foreach (var cube in cubesTaggedForMovement)
            {
                cube.Key.MoveCubeToNode(cube.Value);
            }

            cubesTaggedForMovement.Clear();
        }

        #endregion

        #region GRAVITY

        private void ApplyGravityToBlockedCubes()
        {
            if (baseFallingCube)
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
        }

        private void MakeCubeFall(CubeBehavior_Movement currentCube)
        {
            var currNode = currentCube.cubeBase.currNode;
            int iterations = 1;

            kuboGrid.grid.TryGetValue(currentCube.cubeBase.currCoordinates + (Vector3Int.down * iterations),
                out var nodeUnder);

            // while the node under is free, keep falling
            while (nodeUnder != null && (CubeBehaviors) nodeUnder.cubeType == CubeBehaviors.None)
            {
                currNode = nodeUnder;
                kuboGrid.grid.TryGetValue(currentCube.cubeBase.currCoordinates + (Vector3Int.down * iterations++),
                    out nodeUnder);
                // TODO : falling out of the kubo will undoubtedly break the game
            }

            currentCube.MoveCubeToNode(currNode);
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

        #endregion

        #region Helper Functions

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

        private bool HasCubeUnderOrigin(Vector3Int currCubeCoords)
        {
            // check to see if current cube has a cube underneath
            kuboGrid.grid.TryGetValue(currCubeCoords + Vector3Int.down, out var tempNode);
            if (tempNode == null) return false;
            return ((CubeBehaviors) tempNode.cubeType) != CubeBehaviors.None;
        }

        private bool HasCubeUnderTarget(Vector3Int targetNodeCoords)
        {
            // check to see if target position has a cube underneath
            kuboGrid.grid.TryGetValue(targetNodeCoords + Vector3Int.down, out var tempNode);
            if (tempNode == null) return false;
            return ((CubeBehaviors) tempNode.cubeType) != CubeBehaviors.None;
        }

        #endregion
    }
}