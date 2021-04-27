using UnityEngine;

public class Concrete_VictoryHeavyFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Movement>();
        newObject.AddComponent<CubeBehavior_Heavy>();
        newObject.AddComponent<CubeBehavior_Victory>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}