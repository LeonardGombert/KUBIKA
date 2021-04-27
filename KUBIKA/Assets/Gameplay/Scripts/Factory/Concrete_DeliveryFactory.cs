using UnityEngine;

public class Concrete_DeliveryFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        GameObject newObject = Instantiate(cubePrefab);
        newObject.AddComponent<CubeBehavior_Delivery>();
        finalCube = newObject.GetComponent<CubeCore>();
        return newObject;
    }
}