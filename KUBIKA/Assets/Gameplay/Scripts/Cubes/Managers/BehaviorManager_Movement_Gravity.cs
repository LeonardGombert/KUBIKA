using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    // cube gravity
    public partial class BehaviorManager_Movement
    {
        // target = (moving cube's index - down)
        // OPTION 1 : check a dictionary sorted by index for target
        // OPTION 2 : get and check the grid for contents of target
        
        private void Update()
        {
            foreach (var cube in managedCubes)
            {
               /* if (bIsOpen(cube.myTriCoords + TriCoords.Down))
                {
                    Debug.Log(cube.gameObject.name + " is falling");
                }*/
            }
        }
    }
}