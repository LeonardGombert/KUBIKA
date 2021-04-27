using UnityEngine;

public class Concrete_HeavyFactory : AbstractCubeFactory
{
    public override GameObject ConfigCube(GameObject cubePrefab)
    {
        throw new System.NotImplementedException();
    }
}