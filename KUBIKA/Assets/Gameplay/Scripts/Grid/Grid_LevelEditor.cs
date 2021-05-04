using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class Grid_LevelEditor : AbstractGrid
{
    [SerializeField, ReadOnly] public List<AbstractCubeObject> placedCubes;
    [SerializeField] public int sizeX, sizeY, sizeZ;
    public float width = 1.2f;

    private void Awake() => GenerateNodes();

    // called by the user, once he is finished building the level -> generates the grid based on what was created
    public override void GenerateNodes()
    {
        _nodes  = new GridNode[sizeX * sizeY * sizeZ];

        for (int z = 0, i = 0; z < sizeZ; z++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++, i++)
                {
                    var currentCubeType = ComplexCubeType.None; // default currentType is none
                    var currentPos = new Vector3(x, y, z); // combine into coordinates
                    var currentRot = Vector3.zero;
                    
                    // check if this position is occupied by a placed cube
                    for (int j = 0; j < placedCubes.Count; j++)
                    {
                        if (placedCubes[j].Index.Pos[0] != currentPos) continue;
                        
                        currentCubeType = placedCubes[j].Type;
                        currentRot = placedCubes[j].transform.eulerAngles;
                    }
                    
                    _nodes[i] = new GridNode(x, y, z, currentRot, currentCubeType);
                }
            }
        }
    }

    public override void ClearNodes()
    {
        base.ClearNodes();
        
        for (int i = 0; i < placedCubes.Count; i++)
            if(placedCubes[i])DestroyImmediate(placedCubes[i].gameObject);
    }
}