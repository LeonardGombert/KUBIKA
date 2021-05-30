using UnityEngine;

/// <summary>
/// The tri-coordinate system that keeps track of node positions
/// in all three rotation configurations.
/// </summary>
[System.Serializable]
public struct TriCoords
{
    [SerializeField] private Vector3Int[] pos; 
    public Vector3Int[] Pos => pos;

    public TriCoords(int x, int y, int z)
    {
        pos = new Vector3Int[3];
        pos[0] = new Vector3Int(x, y, z); // position 1
        pos[1] = new Vector3Int(z, x, y); // position 2
        pos[2] = new Vector3Int(y, z, x); // position 3
    }

    public static TriCoords Zero => new TriCoords(0, 0, 0);
}