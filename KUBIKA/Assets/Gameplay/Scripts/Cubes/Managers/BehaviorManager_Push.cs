using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Push : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        public List<CubeBehavior_Movement> cubesToPush = new List<CubeBehavior_Movement>();
        private CubeBehavior_Movement pushedCube;
        public MoveDirection pushDirection;
        private bool canPushCubes;

        Grid_Kubo Kubo => ReferenceProvider.Instance.KuboGrid;

        public void GetPushingCubes(ref CubeBehavior_Movement pushingCube)
        {
            ClearPushingCubes();
            RecursivelyAddPushingCubes(ref pushingCube);
        }

        public void ClearPushingCubes()
        {
            cubesToPush.Clear();
        }

        public bool bCanMovePushingCubes()
        {
            return canPushCubes;
        }

        private void RecursivelyAddPushingCubes(ref CubeBehavior_Movement movingCube)
        {
            cubesToPush.Add(movingCube);
            
            if (Physics.Raycast(movingCube.transform.position, GetPushDirection(), out var hitInfo, 1.25f))
            {
                CubeBehaviour_Base touchedCube = hitInfo.collider.GetComponent<CubeBehaviour_Base>();

                if ((touchedCube.cubeType & ComplexCubeType.Static) != 0)
                {
                    Debug.Log("I'm pushing against a static cube");
                    // then the cube cannot move, nor any of the cubes before it
                    canPushCubes = false;
                    return;
                }

                if ((touchedCube.cubeType & ComplexCubeType.Moveable) != 0)
                {
                    Debug.Log("I'm pushing against a moveable cube");

                    // then there is business to be done. recursively call the function.
                    var pushedCube = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                    RecursivelyAddPushingCubes(ref pushedCube);
                }
                else Debug.Log("I'm not pushing against much of anything... Flags aren't working ?");
            }

            else
            {
                // then there is an available free space, and all nodes can move in that direction
                canPushCubes = true;
                Debug.Log("We can push through");
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