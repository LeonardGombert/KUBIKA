using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Node
{
    public ComplexCubeType cubeType;
    [SerializeField] private Vector3Kubo vector3Kubo;
    [SerializeField] private Vector3 worldPos, worldRot;
    
    public Vector3Int position => vector3Kubo.positions[(int) Grid_Kubo.State];
    public Vector3Int[] positions => vector3Kubo.positions;
    
    public Vector3 worldPosition => worldPos;
    public Vector3 worldRotation => worldRot;

    /// <summary>
    /// Construct a new GridNode.
    /// </summary>
    /// <param name="x">Default Kubo State x position.</param>
    /// <param name="y">Default Kubo State y position.</param>
    /// <param name="z">Default Kubo State z position.</param>
    /// <param name="worldPos">The Node's precise world position.</param>
    /// <param name="worldRot">World Rotation of the Cube at Node Position</param>
    /// <param name="cubeType">The Composite Behaviours of the cube, represented by the ComplexCubeType.</param>
    public Node(int x, int y, int z, Vector3 worldPos, Vector3 worldRot, ComplexCubeType cubeType)
    {
        vector3Kubo = new Vector3Kubo(x, y, z);
        this.worldPos = worldPos;
        this.worldRot = worldRot;
        this.cubeType = cubeType;
    }

    public Node(Vector3Kubo vector3Kubo)
    {
        this.vector3Kubo = vector3Kubo;
        worldPos = Vector3.zero;
        worldRot = Vector3.zero;
        cubeType = ComplexCubeType.None;
    }

    public static Node Zero => new Node(0, 0, 0, Vector3.zero, Vector3.zero, ComplexCubeType.None);
}