using UnityEngine;

public abstract class Abstract_CubeFactory : MonoBehaviour
{
    protected AbstractCubeObject cubeRef;
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
    /// <param name="index">The index in the Grid where the cube will be placed</param>
    /// <returns></returns>
    public virtual AbstractCubeObject PlaceCube(KuboVector index)
    {
        // TODO : place cube
        return null;
    }
}