using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct Node
{
    [SerializeField, ReadOnly] private TriCoords triCoords;
    [SerializeField, ReadOnly] private ComplexCubeType cubeType;
    [SerializeField, ReadOnly] private Vector3 worldPos, rotation;

    /// <summary>
    /// Returns the Coordinates of the GridNode, based on the current Rotational State of the Kubo.
    /// </summary>
    //public Vector3 CurrCoordPos => triCoords.Pos[(int) AbstractGrid.State];
    public TriCoords Coords => triCoords;
    public ComplexCubeType CubeType => cubeType;
    public Vector3 Position => worldPos;
    public Vector3 Rotation => rotation;

    /// <summary>
    /// Construct a new GridNode.
    /// </summary>
    /// <param name="x">Default Kubo State x position.</param>
    /// <param name="y">Default Kubo State y position.</param>
    /// <param name="z">Default Kubo State z position.</param>
    /// <param name="pos">The Node's precise world position.</param>
    /// <param name="rot">World Rotation of the Cube at Node Position</param>
    /// <param name="type">The Composite Behaviours of the cube, represented by the ComplexCubeType.</param>
    public Node(int x, int y, int z, Vector3 pos, Vector3 rot, ComplexCubeType type)
    {
        triCoords = new TriCoords(x, y, z);
        worldPos = pos;
        rotation = rot;
        cubeType = type;
    }
}