using UnityEngine;

namespace Gameplay.Scripts.Factory
{
    public class Concrete_VictoryFactory : AbstractCubeFactory
    {
        public override GameObject ConfigCube(GameObject cubePrefab)
        {
            GameObject newObject = Instantiate(cubePrefab);
            newObject.AddComponent<CubeBehavior_Movement>();
            newObject.AddComponent<CubeBehavior_Victory>();
            finalCube = newObject.GetComponent<CubeCore>();
            return newObject;
        }
    }
}