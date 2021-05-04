using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class GridNode
{
    [SerializeField, ReadOnly] private GridCoord _gridCoord;
    [SerializeField, ReadOnly] private ComplexCubeType _currentComplexCube;
    [SerializeField, ReadOnly] private Vector3 _rotation;

    /// <summary>
    /// Returns the Coordinates of the GridNode, based on the current Rotational State of the Kubo.
    /// </summary>
    public Vector3 CurrPos => _gridCoord.Pos[(int) AbstractGrid.State];

    /// <summary>
    /// Construct a new GridNode.
    /// </summary>
    /// <param name="x">Default Kubo State x position.</param>
    /// <param name="y">Default Kubo State y position.</param>
    /// <param name="z">Default Kubo State z position.</param>
    /// <param name="rotation">World Rotation of the Cube at Node Position</param>
    /// <param name="currentComplexCube">The Composite Behaviours of the cube, represented by the ComplexCubeType.</param>
    public GridNode(int x, int y, int z, Vector3 rotation, ComplexCubeType currentComplexCube)
    {
        _gridCoord = new GridCoord(x, y, z);
        _rotation = rotation;
        _currentComplexCube = currentComplexCube;
    }
}