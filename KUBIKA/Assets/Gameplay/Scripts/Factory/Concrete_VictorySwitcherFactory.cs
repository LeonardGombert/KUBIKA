using UnityEngine;

class Concrete_VictorySwitcherFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Switcher>();
        newObject.AddComponent<CubeBehavior_Victory>();
        newObject.AddComponent<CubeBehavior_Movement>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}