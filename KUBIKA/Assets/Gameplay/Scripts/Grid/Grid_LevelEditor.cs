using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class Grid_LevelEditor : AbstractGrid
{
    [SerializeField, ReadOnly] public List<AbstractCubeObject> placedCubes;
    [SerializeField, ReadOnly] public int sizeX, sizeY, sizeZ;

    private void Awake() => GenerateNodes();

    // Called by the user, once he is finished building the level -> generates the grid based on what was created
    public override void GenerateNodes()
    {
        Nodes = new Node[sizeX, sizeY, sizeZ];

        for (int z = 0; z < sizeZ; z++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    var currentCubeType = ComplexCubeType.None; // default currentType is none
                    var currentPos = new Vector3(x, y, z); // combine into coordinates
                    var currentRot = Vector3.zero;

                    // check if this position is occupied by a placed cube
                    for (int j = 0; j < placedCubes.Count; j++)
                    {
                        if (placedCubes[j].Coords.Pos[0] != currentPos) continue;

                        currentCubeType = placedCubes[j].CubeType;
                        currentRot = placedCubes[j].transform.eulerAngles;
                    }

                    Nodes[x, y, z] = new Node(x, y, z, currentPos * width, currentRot, currentCubeType);
                }
            }
        }
    }

    public override void ClearNodes()
    {
        base.ClearNodes();

        for (int i = 0; i < placedCubes.Count; i++)
            if (placedCubes[i])
                DestroyImmediate(placedCubes[i].gameObject);

        placedCubes.Clear();
    }
}