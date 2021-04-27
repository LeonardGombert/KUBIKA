using UnityEngine;

public class Concrete_VictoryHeavyFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        finalCube = newObject.GetComponent<Cube_Core>();
        finalCube.OnSpawn(
            newObject.AddComponent<CubeBehavior_Movement>(), 
            newObject.AddComponent<CubeBehavior_Victory>(), 
            newObject.AddComponent<CubeBehavior_Heavy>());
        return newObject;
    }
}