using UnityEngine;

class Concrete_CounterFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Counter>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}