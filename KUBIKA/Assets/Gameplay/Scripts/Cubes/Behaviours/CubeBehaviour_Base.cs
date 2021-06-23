using Sirenix.OdinInspector;
using UnityEngine;

public class CubeBehaviour_Base : AbstractCubeBehavior
{
    [SerializeField] public Node currNode;
    public ComplexCubeType cubeType;
    
    /// <summary>
    /// Returns the Cube's current coordinates relative to the Kubo's rotation.
    /// </summary>
    public Vector3Int CurrPosition => currNode.position;
    public Vector3Int[] gridPosition => currNode.positions;

    /// <summary>
    /// Call when building a level. Is in charge of setting up the cube's position, rotation, type, etc. once it is instantiated.
    /// </summary>
    /// <param name="currNode">The Cube's TriCoords.</param>
    /// <param name="cubeType">The Cube's type.</param>
    /// <param name="position">The Cube's starting position.</param>
    /// <param name="parent">The Cube's transform parent.</param>
    public void ConfigCube(Node currNode, ComplexCubeType cubeType, Vector3 position, Transform parent)
    {
        this.currNode = currNode;
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
    public void ConfigCube(Node currNode, ComplexCubeType cubeType)
    {
        this.currNode = currNode;
        this.cubeType = cubeType;
    }
}