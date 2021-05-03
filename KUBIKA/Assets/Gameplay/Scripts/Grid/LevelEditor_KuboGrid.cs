using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class LevelEditor_KuboGrid : AbstractKuboGrid
{
    [SerializeField, ReadOnly] public List<AbstractCubeObject> placedCubes;
    [SerializeField] private int sizeX, sizeY, sizeZ;
    public float width = 1.2f;

    private void Awake() => BuildGrid();

    // called by the user, once he is finished building the level -> generates the grid based on what was created
    public override void BuildGrid()
    {
        Grid = new KuboNode[sizeX * sizeY * sizeZ];

        for (int z = 0, i = 0; z < sizeZ; z++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++, i++)
                {
                    var currentCubeType = CubeType.None; // default currentType is none
                    var currentPos = new Vector3(x, y, z); // combine into coordinates
                    
                    // check if this position is occupied by a placed cube
                    for (int j = 0; j < placedCubes.Count; j++)
                    {
                        if (placedCubes[j].Index.Config[0] != currentPos) continue;
                        
                        currentCubeType = placedCubes[j].Type;
                        //placedCubes.RemoveAt(j);
                    }
                    
                    Grid[i] = new KuboNode(x, y, z, currentCubeType);
                }
            }
        }
    }

    public void ClearGrid()
    {
        Grid = null;
        
        for (int i = 0; i < placedCubes.Count; i++)
            DestroyImmediate(placedCubes[i].gameObject);
    }
}