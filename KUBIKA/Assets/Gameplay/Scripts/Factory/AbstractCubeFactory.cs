using UnityEngine;

public abstract class AbstractCubeFactory : MonoBehaviour
{
    [SerializeField] protected GameObject cubePrefab;

    /// <summary>
    /// Instantiate and place a new cube of the relevant CubeType. Returns a reference to the placed cube.
    /// </summary>
    /// <param name="index">The Vector3 position corresponding to the index where the cube will be placed</param>
    /// <returns></returns>
    public virtual GameObject SpawnCube()
    {
        return Instantiate(cubePrefab);
    }
}