using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public abstract class AbstractBehaviorManager<T> : MonoBehaviour where T : AbstractCubeBehavior
    {
        [SerializeField] protected List<T> managedCubes = new List<T>();

        private void Start()
        {
            OnLevelLoaded();
        }

        private void OnLevelLoaded()
        {
            managedCubes.Clear();
            managedCubes.AddRange(FindObjectsOfType<T>());
            
            foreach (var cube in managedCubes)
            {
                cube.InitBehavior();
            }
        }
    }
}