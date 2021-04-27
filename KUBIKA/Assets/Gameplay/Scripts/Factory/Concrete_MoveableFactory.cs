using UnityEngine;

class Concrete_MoveableFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Movement>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}