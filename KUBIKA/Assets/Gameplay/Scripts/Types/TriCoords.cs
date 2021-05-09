using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public struct TriCoords
{
    [SerializeField] private Vector3Int[] _pos; 
    public Vector3Int[] Pos => _pos;

    public TriCoords(int x, int y, int z)
    {
        _pos = new Vector3Int[3];
        _pos[0] = new Vector3Int(x, y, z); // position 1
        _pos[1] = new Vector3Int(z, x, y); // position 2
        _pos[2] = new Vector3Int(y, z, x); // position 3
    }

    public static TriCoords Zero => new TriCoords(0, 0, 0);
}