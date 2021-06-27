using System;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Gravity : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        // target = (moving cube's index - down)
        // OPTION 1 : check a dictionary sorted by index for target
        // OPTION 2 : get and check the grid for contents of target

        public void CheckCubeGravity(CubeBehavior_Movement cube)
        {
            ReferenceProvider.Instance.KuboGrid.grid.TryGetValue(cube.cubeBase.currCoordinates + Vector3Int.down, out var targetNode);

            if (targetNode != null && targetNode.cubeType == ComplexCubeType.None)
            {
                cube.bMovingCubeToNode(ref targetNode);
                
                // recursively calls the function until there are no more carried cubes 
                if (cube.carrying)
                {
                    CheckCubeGravity(cube.carrying);
                }
                CheckCubeGravity(cube);
            }
        }
    }
}