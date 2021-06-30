using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Carry : MonoBehaviour
    {
        public bool bAreThereAnyCubesAbove(ref CubeBehavior_Movement cubeBehaviorMovement)
        {
            // is there a carriedCube in the moving cube ? 
            // no -> do nothing

            // yes -> then call AreThereCubesInTheWayOf(carriedCube)
            if (cubeBehaviorMovement.carrying != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}