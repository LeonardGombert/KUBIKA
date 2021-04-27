using UnityEngine;

class Concrete_VictoryMineFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Movement>();
        newObject.AddComponent<CubeBehavior_Victory>();
        newObject.AddComponent<CubeBehavior_Mine>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}