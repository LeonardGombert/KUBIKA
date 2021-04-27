using UnityEngine;

class Concrete_SwitcherFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        finalCube = newObject.GetComponent<Cube_Core>();
        finalCube.OnSpawn(
            newObject.AddComponent<CubeBehavior_Switcher>(),
            newObject.AddComponent<CubeBehavior_Movement>());
        return newObject;
    }
}