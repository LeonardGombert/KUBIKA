using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Carry : MonoBehaviour
    {
        public CubeBehavior_Movement cubeToMove;
        public List<CubeBehavior_Movement> cubesStack = new List<CubeBehavior_Movement>();

        public void GetCarriedCubes(ref CubeBehavior_Movement movingCube)
        {
            cubeToMove = movingCube;
            cubesStack.Clear();
            RecursivelyFindCubesToMove();
        }

        private void RecursivelyFindCubesToMove()
        {
            cubesStack.Add(cubeToMove);

            if (cubeToMove.carryingCube != null)
            {
                cubeToMove = cubeToMove.carryingCube;
                RecursivelyFindCubesToMove();
            }
        }
    }
}