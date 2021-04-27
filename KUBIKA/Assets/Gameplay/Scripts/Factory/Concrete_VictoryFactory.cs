using UnityEngine;

namespace Gameplay.Scripts.Factory
{
    public class Concrete_VictoryFactory : AbstractCubeFactory
    {
        public override GameObject ConfigCube(GameObject cubePrefab)
        {
            GameObject newObject = Instantiate(cubePrefab);
            finalCube = newObject.GetComponent<Cube_Core>();
            finalCube.OnSpawn(
                newObject.AddComponent<CubeBehavior_Movement>(),
                newObject.AddComponent<CubeBehavior_Victory>());
            return newObject;
        }
    }
}