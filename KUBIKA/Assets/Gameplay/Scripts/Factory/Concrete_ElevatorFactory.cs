using UnityEngine;

class Concrete_ElevatorFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Elevator>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}