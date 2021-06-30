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
                canPushCubes = true;
            }
        }

        private Vector3 GetPushDirection()
        {
            switch (pushDirection)
            {
                case MoveDirection.Forward:
                    return Vector3.forward;
                case MoveDirection.Right:
                    return Vector3.right;
                case MoveDirection.Back:
                    return Vector3.back;
                case MoveDirection.Left:
                    return Vector3.left;
                case MoveDirection.Down:
                    return Vector3.down;
                default:
                    return Vector3.zero;
            }
        }

    }
}