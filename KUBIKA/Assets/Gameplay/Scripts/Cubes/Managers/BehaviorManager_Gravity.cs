using System;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Gravity : MonoBehaviour
    {
        public void CheckCubeGravity(CubeBehavior_Movement cube)
        {
            ReferenceProvider.Instance.KuboGrid.grid.TryGetValue(cube.cubeBase.currCoordinates + Vector3Int.down,
                out var targetNode);

            if (targetNode != null && targetNode.cubeType == ComplexCubeType.None)
            {
                cube.MoveCubeToNode(ref targetNode);

                // recursively calls the function until there are no more carried cubes 
                if (cube.carrying)
                {
                    CheckCubeGravity(cube.carrying);
                }
            }
        }
    }
}