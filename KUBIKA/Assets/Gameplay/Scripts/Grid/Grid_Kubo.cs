using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Grid_Kubo : SerializedMonoBehaviour
{
    [ShowInInspector, ReadOnly, TableList] public static KuboState State;
    [SerializeField] private int sizeX, sizeY, sizeZ;

    [ReadOnly, TableList] public Dictionary<TriCoords, CubeBehaviors> nodeDictionary;

    public void NodesToDictionary(Node[,,] newGrid)
    {
        nodeDictionary = new Dictionary<TriCoords, CubeBehaviors>();

        for (int x = 0; x < newGrid.GetLength(0); x++)
        {
            for (int y = 0; y < newGrid.GetLength(1); y++)
            {
                for (int z = 0; z < newGrid.GetLength(2); z++)
                {
                    Node currNode = newGrid[x, y, z];
                    nodeDictionary.Add(currNode.Coords, (CubeBehaviors)currNode.CubeType);
                }
            }
        }
    }
}