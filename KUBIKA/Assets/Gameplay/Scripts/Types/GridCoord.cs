using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public struct GridCoord
{
    [ShowInInspector] public Vector3[] Pos { get; private set; }

    public GridCoord(int x, int y, int z)
    {
        Pos = new Vector3[3];
        Pos[0] = new Vector3(x, y, z); // position 1
        Pos[1] = new Vector3(z, x, y); // position 2
        Pos[2] = new Vector3(y, z, x); // position 3
    }

    public static GridCoord Zero => new GridCoord(0, 0, 0);
}