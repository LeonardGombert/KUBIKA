using UnityEngine;

public abstract class AbstractCubeFactory : MonoBehaviour
{
    protected Cube_Core cubeRef;
    [SerializeField] protected GameObject cubePrefab;

    protected virtual GameObject ConfigCube() { return Instantiate(cubePrefab); }

    public virtual Cube_Core PlaceCube(int index)
    {
        (cubeRef = ConfigCube().GetComponent<Cube_Core>())._index = index;
        return cubeRef;
    }
}