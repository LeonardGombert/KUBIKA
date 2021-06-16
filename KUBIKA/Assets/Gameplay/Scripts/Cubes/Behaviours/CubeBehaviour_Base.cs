using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehaviour_Base : AbstractCubeBehavior
{
    [SerializeField, ReadOnly] protected TriCoords gridPosition;
    [SerializeField] protected ComplexCubeType cubeType;

    // public getters. Sets default to protected.
    public TriCoords TriCoords
    {
        get => gridPosition;
        set => gridPosition = value;
    }

    public ComplexCubeType CubeType => cubeType;

    /// <summary>
    /// Returns the Cube's current coordinates relative to the Kubo's rotation.
    /// </summary>
    public Vector3Int CurrPosition => gridPosition.Pos[(int) Grid_Kubo.State];

    /// <summary>
    /// Call when building a level. Is in charge of setting up the cube's position, rotation, type, etc. once it is instantiated.
    /// </summary>
    /// <param name="tricoords">The Cube's TriCoords.</param>
    /// <param name="complexCubeType">The Cube's type.</param>
    /// <param name="position">The Cube's starting position.</param>
    /// <param name="parent">The Cube's transform parent.</param>
    public void ConfigCube(TriCoords tricoords, ComplexCubeType complexCubeType, Vector3 position, Transform parent)
    {
        gridPosition = tricoords;
        cubeType = complexCubeType;
        var @ref = transform;
        @ref.position = position;
        @ref.parent = parent;
    }

    /// <summary>
    /// Config cube function overload for Level Editor Window.
    /// </summary>
    /// <param name="coords"></param>
    /// <param name="complexCubeType"></param>
    public void ConfigCube(TriCoords coords, ComplexCubeType complexCubeType)
    {
        gridPosition = coords;
        cubeType = complexCubeType;
    }
}