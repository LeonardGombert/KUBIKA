using UnityEngine;

class Concrete_RotatorFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Rotator>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}