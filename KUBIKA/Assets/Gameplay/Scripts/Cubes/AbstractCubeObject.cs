using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] protected TriCoords gridPosition = TriCoords.Zero;
    [SerializeField] protected ComplexCubeType cubeType;

    // public getters for Cube Coords and CubeType. Sets are protected above^.
    public TriCoords Coords => gridPosition;
    public ComplexCubeType CubeType => cubeType;

    /// <summary>
    /// Call when building a level. Is in charge of setting up the cube's position, rotation, type, etc. once it is instantiated.
    /// </summary>
    /// <param name="coords">The Cube's TriCoords.</param>
    /// <param name="complexCubeType">The Cube's type.</param>
    /// <param name="position">The Cube's starting position.</param>
    /// <param name="parent">The Cube's transform parent.</param>
    public void ConfigCube(TriCoords coords, ComplexCubeType complexCubeType, Vector3 position, Transform parent)
    {
        gridPosition = coords;
        cubeType = complexCubeType;
        var @ref = transform;
        @ref.position = position;
        @ref.parent = parent;
    }
}