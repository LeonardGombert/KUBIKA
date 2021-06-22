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
    
    [SerializeField] private Vector3Int[,,] pos1;
    [SerializeField] private Vector3Int[,,] pos2;
    [SerializeField] private Vector3Int[,,] pos3;

    // default constructor
    public TriCoords(int x, int y, int z)
    {
        pos = new Vector3Int[3];
        pos[0] = new Vector3Int(x, y, z); // position 1
        pos[1] = new Vector3Int(z, x, y); // position 2
        pos[2] = new Vector3Int(y, z, x); // position 3

        pos1 = new Vector3Int[x,y,z];
        pos2 = new Vector3Int[z,x,y];
        pos3 = new Vector3Int[y,z,x];
    }

    // copy constructor
    public TriCoords(TriCoords copy)
    {
        pos = copy.Pos;
        pos[0] = copy.Pos[0];
        pos[1] = copy.Pos[1];
        pos[2] = copy.Pos[2];
        
        pos1 = copy.pos1;
        pos2 = copy.pos2;
        pos3 = copy.pos3;
    }
    
    public static TriCoords Zero => new TriCoords(0, 0, 0);
    public static TriCoords Forward => new TriCoords(0, 0, 1);
    public static TriCoords Right => new TriCoords(1, 0, 0);
    public static TriCoords Back => new TriCoords(0, 0, -1);
    public static TriCoords Left => new TriCoords(-1, 0, 0);
    public static TriCoords Down => new TriCoords(0, -1, 0);

    public static TriCoords operator +(TriCoords lhs, TriCoords rhs)
    {
        return new TriCoords(lhs.pos[0].x + rhs.pos[0].x, lhs.pos[0].y + rhs.pos[0].y, lhs.pos[0].z + rhs.pos[0].z);
    }

    public static TriCoords operator +(TriCoords lhs, Vector3Int rhs)
    {
        return new TriCoords(lhs.pos[0].x + rhs.x, lhs.pos[0].y + rhs.y, lhs.pos[0].z + rhs.z);
        /*Debug.Log("LHS : " + new Vector3(lhs.pos[0].x, lhs.pos[0].y, lhs.pos[0].z));
        Debug.Log("RHS : " + rhs);
        
        lhs.pos[0].x += rhs.x;
        lhs.pos[0].y += rhs.y;
        lhs.pos[0].z += rhs.z;

        lhs.pos[1].x += rhs.z;
        lhs.pos[1].y += rhs.x;
        lhs.pos[1].z += rhs.y;

        lhs.pos[2].x += rhs.y;
        lhs.pos[2].y += rhs.z;
        lhs.pos[2].z += rhs.x;

        Debug.Log("FINAL VALUES : " + new Vector3(lhs.pos[0].x, lhs.pos[0].y, lhs.pos[0].z));
        return lhs;*/
    }
}