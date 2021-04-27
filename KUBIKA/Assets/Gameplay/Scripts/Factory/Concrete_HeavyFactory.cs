using UnityEngine;

public class Concrete_HeavyFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Movement>();
        newObject.AddComponent<CubeBehavior_Heavy>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}
