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
    public static TriCoords Forward => new TriCoords(0, 0, 1);
    public static TriCoords Right => new TriCoords(1, 0, 0);
    public static TriCoords Back => new TriCoords(0, 0, -1);
    public static TriCoords Left => new TriCoords(-1, 0, 0);

    public static TriCoords operator +(TriCoords lhs, Vector3Int rhs)
    {
        Debug.Log("LHS : " + new Vector3(lhs.pos[0].x, lhs.pos[0].y, lhs.pos[0].z));
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

        /*lhs.pos[0].x += rhs.pos[0].x;
        lhs.pos[0].y += rhs.pos[0].y;
        lhs.pos[0].z += rhs.pos[0].z;

        lhs.pos[1].x += rhs.pos[1].x;
        lhs.pos[1].y += rhs.pos[1].y;
        lhs.pos[1].z += rhs.pos[1].z;

        lhs.pos[2].x += rhs.pos[2].x;
        lhs.pos[2].y += rhs.pos[2].y;
        lhs.pos[2].z += rhs.pos[2].z;*/

        Debug.Log("FINAL VALUES : " + new Vector3(lhs.pos[0].x, lhs.pos[0].y, lhs.pos[0].z));

        return lhs;
    }
}