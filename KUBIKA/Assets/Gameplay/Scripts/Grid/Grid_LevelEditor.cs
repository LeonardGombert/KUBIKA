using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_LevelEditor : AbstractGrid
{
    [SerializeField] int sizeX, sizeY, sizeZ;
    private int gridSize;

    private void Awake() => BuildGrid();

    public override void BuildGrid()
    {
        _gridNodes = new GridNode[sizeX * sizeY * sizeZ];

        for (int z = 0, i = 0; z < sizeZ; z++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++, i++)
                {
                    _gridNodes[i] = new GridNode(x, y, z, CubeType.None);
                }
            }
        }
    }

    public override void RotateGrid()
    {
        throw new System.NotImplementedException();
    }

    public override void ResetGrid()
    {
        throw new System.NotImplementedException();
    }
}