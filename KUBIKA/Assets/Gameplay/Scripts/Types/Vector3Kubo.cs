using UnityEngine;

/// <summary>
/// The tri-coordinate system that keeps track of node positions
/// in all three rotation configurations.
/// </summary>
[System.Serializable]
public struct Vector3Kubo
{
    [SerializeField] private Vector3Int[] pos;
    public Vector3Int[] positions => pos;
    
    // default constructor
    public Vector3Kubo(int x, int y, int z)
    {
        pos = new Vector3Int[3];
        pos[0] = new Vector3Int(x, y, z); // position 1
        pos[1] = new Vector3Int(z, x, y); // position 2
        pos[2] = new Vector3Int(y, z, x); // position 3
    }

    // copy constructor
    public Vector3Kubo(Vector3Kubo copy)
    {
        pos = copy.positions;
        pos[0] = copy.positions[0];
        pos[1] = copy.positions[1];
        pos[2] = copy.positions[2];
    }
    
    public static Vector3Kubo Zero => new Vector3Kubo(0, 0, 0);
    public static Vector3Kubo Forward => new Vector3Kubo(0, 0, 1);
    public static Vector3Kubo Right => new Vector3Kubo(1, 0, 0);
    public static Vector3Kubo Back => new Vector3Kubo(0, 0, -1);
    public static Vector3Kubo Left => new Vector3Kubo(-1, 0, 0);
    public static Vector3Kubo Down => new Vector3Kubo(0, -1, 0);

    public static Vector3Kubo operator +(Vector3Kubo lhs, Vector3Kubo rhs)
    {
        return new Vector3Kubo(lhs.pos[0].x + rhs.pos[0].x, lhs.pos[0].y + rhs.pos[0].y, lhs.pos[0].z + rhs.pos[0].z);
    }

    public static Vector3Kubo operator +(Vector3Kubo lhs, Vector3Int rhs)
    {
        return new Vector3Kubo(lhs.pos[0].x + rhs.x, lhs.pos[0].y + rhs.y, lhs.pos[0].z + rhs.z);
    }
    
    public static implicit operator Vector3Kubo(Vector3Int position)
    {
        return new Vector3Kubo(position.x, position.y, position.z);
    }
}