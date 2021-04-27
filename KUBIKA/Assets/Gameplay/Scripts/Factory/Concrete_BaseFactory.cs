using UnityEngine;

class Concrete_BaseFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        return Instantiate(cubePrefab);
    }
}