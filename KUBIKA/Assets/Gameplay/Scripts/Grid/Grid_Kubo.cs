using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Grid_Kubo : SerializedMonoBehaviour
{
    [ShowInInspector, ReadOnly, TableList] public static KuboState State;
    [SerializeField] private int sizeX, sizeY, sizeZ;

    [ReadOnly, TableList] public Dictionary<Vector3Int, Node> config1NodeDictionary;
    [ReadOnly, TableList] public Dictionary<Vector3Int, Node> config2NodeDictionary;
    [ReadOnly, TableList] public Dictionary<Vector3Int, Node> config3NodeDictionary;

    public void NodesToDictionary(Node[,,] newGrid)
    {
        config1NodeDictionary = new Dictionary<Vector3Int, Node>();
        config2NodeDictionary = new Dictionary<Vector3Int, Node>();
        config3NodeDictionary = new Dictionary<Vector3Int, Node>();

        for (int x = 0; x < newGrid.GetLength(0); x++)
        {
            for (int y = 0; y < newGrid.GetLength(1); y++)
            {
                for (int z = 0; z < newGrid.GetLength(2); z++)
                {
                    Node currNode = newGrid[x, y, z];
                    config1NodeDictionary.Add(currNode.Coords.Pos[0], currNode);
                    config2NodeDictionary.Add(currNode.Coords.Pos[1], currNode);
                    config3NodeDictionary.Add(currNode.Coords.Pos[2], currNode);
                }
            }
        }
    }
}