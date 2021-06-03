using UnityEngine;
using UnityEngine.TextCore;

[System.Serializable]
public struct SaveFile
{
    // [SerializeField] private string _kubicode;
    // [SerializeField] private Biome _biome;
    // [SerializeField] private int _minimumMoves;
    // [SerializeField] private bool _lockRotate;

    [SerializeField] private string levelName;
    [SerializeField] private int x, y, z;
    [SerializeField] private Node[] flatNodes;

    public SaveFile(Node[,,] nodes, string levelName)
    {
        x = nodes.GetLength(0);
        y = nodes.GetLength(1);
        z = nodes.GetLength(2);
        
        flatNodes = FlattenArray(x, y, z, nodes);
        this.levelName = levelName;
    }

    public static Node[] FlattenArray(int x, int y, int z, Node[,,] nodes)
    {
        Node[] flatArray = new Node[x * y * z];

        for (int _z = 0, index = 0; _z < z; _z++)
        {
            for (int _y = 0; _y < y; _y++)
            {
                for (int _x = 0; _x < x; _x++, index++)
                {
                    flatArray[index] = nodes[_x, _y, _z];
                }
            }
        }

        return flatArray;
    }

    public static Node[,,] StretchArray(int x, int y, int z, Node[] flatNodes)
    {
        Node[,,] stretchedArray = new Node[x, y, z];

        for (int _z = 0; _z < z; _z++)
        {
            for (int _y = 0; _y < y; _y++)
            {
                for (int _x = 0; _x < x; _x++)
                {
                    // x = +1 
                    // y = +the total width
                    // z = +the total width*height 
                    stretchedArray[_x, _y, _z] = flatNodes[(_x) + (x * _y) + ((x * y) * _z)];
                }
            }
        }

        return stretchedArray;
    }

    /// <summary>
    /// Returns the stored name of the Level.
    /// </summary>
    public string Name => levelName;

    /// <summary>
    /// Returns the array of Nodes that makes up the level's grid.
    /// </summary>
    public Node[,,] Nodes => StretchArray(x, y, z, flatNodes);

    public Node[] FlatNodes => flatNodes;
}