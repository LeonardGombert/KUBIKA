using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Push : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        public List<CubeBehavior_Movement> pushingCubes = new List<CubeBehavior_Movement>();
        private CubeBehavior_Movement pushedCube;
        
        public void ListPushingCubes(ref CubeBehavior_Movement pushedCube)
        {
            pushingCubes.Clear();
            RecursivelyAddPushingCubes(ref pushedCube);
        }

        private void RecursivelyAddPushingCubes(ref CubeBehavior_Movement cubeBehaviorMovement)
        {
            pushingCubes.Add(cubeBehaviorMovement);

            var behaviorMovement = cubeBehaviorMovement.pushing;
            if (behaviorMovement != null)
            {
                RecursivelyAddPushingCubes(ref behaviorMovement);
            }
        }

        public Vector3 GetPushDirection(MoveDirection moveDirection)
        {
            switch (moveDirection)
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