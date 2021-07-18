using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Grid_Kubo : SerializedMonoBehaviour
{
    public Dictionary<Vector3Int, Node> grid;

    public void FillDictionaryWithLoadedNodes(Node[,,] newGrid)
    {
        grid = new Dictionary<Vector3Int, Node>();
        
        for (int x = 0; x < newGrid.GetLength(0); x++)
        {
            for (int y = 0; y < newGrid.GetLength(1); y++)
            {
                for (int z = 0; z < newGrid.GetLength(2); z++)
                {
                    grid.Add(new Vector3Int(x, y, z), newGrid[x, y, z]);
                }
            }
        }
    }
}