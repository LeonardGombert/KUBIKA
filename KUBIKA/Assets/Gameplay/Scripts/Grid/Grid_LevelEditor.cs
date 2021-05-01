using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_LevelEditor : AbstractGrid
{
    [SerializeField] int sizeX, sizeY, sizeZ;

    [Space, Header("Testing")] 
    [SerializeField] GameObject cubePrefab;

    [SerializeField] private float width = 1.2f;

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
                    Instantiate(cubePrefab, new Vector3(x * width, y * width, z * width), Quaternion.identity, transform);
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