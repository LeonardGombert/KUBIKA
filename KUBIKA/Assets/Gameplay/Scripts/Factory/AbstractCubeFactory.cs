using UnityEngine;

public abstract class AbstractCubeFactory : MonoBehaviour
{
   // protected AbstractCubeObject cubeRef;
    [SerializeField] protected GameObject cubePrefab;

    /// <summary>
    /// Instantiate the cube prefab of the correct CubeType. It prefab will automatically contain
    /// the relevant visuals and behaviour scripts.
    /// </summary>
    /// <returns></returns>
    protected virtual GameObject ConfigCube() { return Instantiate(cubePrefab); }

    /// <summary>
    /// Instantiate and place a new cube of the relevant CubeType. Returns a reference to the placed cube.
    /// </summary>
    /// <param name="index">The Vector3 position corresponding to the index where the cube will be placed</param>
    /// <returns></returns>
    public virtual AbstractCubeObject PlaceCube(Vector3 index)
    {
        ConfigCube().transform.position = index;
        return null;
    }
}