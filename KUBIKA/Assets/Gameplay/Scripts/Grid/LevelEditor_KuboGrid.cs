using UnityEditor;
using UnityEngine;

public class LevelEditor_KuboGrid : AbstractKuboGrid
{
    [SerializeField] private int sizeX, sizeY, sizeZ;

    [Space, Header("Testing")] 
    
    [SerializeField] private GameObject cubePrefab;
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
                    Grid[i] = new KuboNode(x, y, z, CubeType.None);
                    // Instantiate(cubePrefab, new Vector3(x * width, y * width, z * width), Quaternion.identity, transform);
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