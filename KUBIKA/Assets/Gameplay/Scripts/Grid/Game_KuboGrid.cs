using UnityEngine;

public class Game_KuboGrid : AbstractKuboGrid
{
    [SerializeField] int sizeX, sizeY, sizeZ;

    private void Awake() => BuildGrid();

    public override void BuildGrid()
    {
        grid = new KuboNode[sizeX * sizeY * sizeZ];

        for (int z = 0, i = 0; z < sizeZ; z++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++, i++)
                {
                    grid[i] = new KuboNode(x, y, z, CubeType.None);
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