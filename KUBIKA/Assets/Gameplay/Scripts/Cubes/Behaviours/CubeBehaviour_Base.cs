using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehaviour_Base : AbstractCubeBehavior
{
    public Node currNode;
    public Vector3Int currCoordinates;
    public ComplexCubeType cubeType;
    public Vector3 currWorldPosition => transform.position;

    /// <summary>
    /// Call when building a level. Is in charge of setting up the cube's position, rotation, type, etc. once it is instantiated.
    /// </summary>
    /// <param name="currNode">The Cube's TriCoords.</param>
    /// <param name="cubeType">The Cube's type.</param>
    /// <param name="position">The Cube's starting position.</param>
    /// <param name="parent">The Cube's transform parent.</param>
    public void ConfigCube(Node currNode, Vector3Int currCoordinates, ComplexCubeType cubeType, Vector3 position,
        Transform parent)
    {
        this.currNode = currNode;
        this.currCoordinates = currCoordinates;
        this.cubeType = cubeType;
        var @ref = transform;
        @ref.position = position;
        @ref.parent = parent;
    }

    /// <summary>
    /// Config cube function overload for Level Editor Window.
    /// </summary>
    /// <param name="currNode"></param>
    /// <param name="cubeType"></param>
    public void ConfigCube(Node currNode, Vector3Int currCoordinates, ComplexCubeType cubeType)
    {
        this.currNode = currNode;
        this.currCoordinates = currCoordinates;
        this.cubeType = cubeType;
    }
}