using Sirenix.OdinInspector;
using UnityEngine;

public class CubePoolManager_LevelEditor : AbstractCubePoolManager
{
    [ShowInInspector, ReadOnly] private Grid_LevelEditor GridLevelEditor => FindObjectOfType<Grid_LevelEditor>();

    // called by Level Editor Window
    public GameObject PlaceCube(CubeBehaviors type) => _cubeFactories[type].SpawnCube();

    public override void AssembleLevel(Node[,,] grid)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    if (grid[x, y, z].CubeTypeType == ComplexCubeType.None) continue;

                    // hell yes
                    var newCube = _cubeFactories[(CubeBehaviors) grid[x, y, z].CubeTypeType].SpawnCube();

                    var cubeObject = newCube.GetComponent<CubeObject_LevelEditor>();

                    cubeObject.ConfigCube(grid[x, y, z].Coords, grid[x, y, z].CubeTypeType, grid[x, y, z].Position,
                        GridLevelEditor.transform);

                    GridLevelEditor.placedCubes.Add(cubeObject);
                }
            }
        }
    }
}