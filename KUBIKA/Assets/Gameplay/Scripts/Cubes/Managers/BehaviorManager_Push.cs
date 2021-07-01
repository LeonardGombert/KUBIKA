using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Push : MonoBehaviour
    {
        public List<CubeBehavior_Movement> cubesToPush = new List<CubeBehavior_Movement>();
        private CubeBehavior_Movement pushedCube;
        public MoveDirection pushDirection;
        private bool canPushCubes;

        public void ListCubesToPush(ref CubeBehavior_Movement pushingCube)
        {
            ClearPushingCubes();
            RecursivelyAddPushingCubes(ref pushingCube);
        }

        public void ReversePushingCubesList() => cubesToPush.Reverse();

        public void ClearPushingCubes() => cubesToPush.Clear();

        public bool bCanMovePushingCubes() => canPushCubes;

        private void RecursivelyAddPushingCubes(ref CubeBehavior_Movement movingCube)
        {
            cubesToPush.Add(movingCube);

            if (Physics.Raycast(movingCube.transform.position, GetPushDirection(), out var hitInfo, 1.25f))
            {
                CubeBehaviour_Base touchedCube = hitInfo.collider.GetComponent<CubeBehaviour_Base>();

                if ((touchedCube.cubeType & ComplexCubeType.Static) != 0)
                {
                    // then the cube cannot move, nor any of the cubes before it
                    canPushCubes = false;
                    cubesToPush.Clear();
                    return;
                }

                if ((touchedCube.cubeType & ComplexCubeType.Moveable) != 0)
                {
                    // then there is business to be done. recursively call the function.
                    var pushedCube = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                    RecursivelyAddPushingCubes(ref pushedCube);
                }
            }

            else
            {
                // then there is an available free space, and all nodes can move in that direction
                canPushCubes = CheckIfFree();
            }
        }

        private bool CheckIfFree()
        {
            ReferenceProvider.Instance.KuboGrid.grid.TryGetValue(
                cubesToPush[cubesToPush.Count - 1].cubeBase.currCoordinates + GetPushDirection(),
                out var targetNode);

            if (targetNode == null)
                return false;
            return true;
        }

        private Vector3Int GetPushDirection()
        {
            switch (pushDirection)
            {
                case MoveDirection.Forward:
                    return Vector3Int.forward;
                case MoveDirection.Right:
                    return Vector3Int.right;
                case MoveDirection.Back:
                    return Vector3Int.back;
                case MoveDirection.Left:
                    return Vector3Int.left;
                case MoveDirection.Down:
                    return Vector3Int.down;
                default:
                    return Vector3Int.zero;
            }
        }
    }
}