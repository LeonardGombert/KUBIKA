using Sirenix.OdinInspector;
using UnityEngine;

public class CubePoolManager_LevelEditor : AbstractCubePoolManager
{
    [ShowInInspector, ReadOnly] private Grid_LevelEditor GridLevelEditor => FindObjectOfType<Grid_LevelEditor>();

    // called by Level Editor Window
    public GameObject PlaceCube(CubeBehaviors type) => _cubeFactories[type].SpawnCube();

    public override void AssembleLevel(Node[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].CubeTypeType == ComplexCubeType.None) continue;

            // hell yes
            var newCube = _cubeFactories[(CubeBehaviors) grid[i].CubeTypeType].SpawnCube();

            var cubeObject = newCube.GetComponent<CubeObject_LevelEditor>();

            cubeObject.ConfigCube(grid[i].Coords, grid[i].CubeTypeType, grid[i].Position, GridLevelEditor.transform);

            GridLevelEditor.placedCubes.Add(cubeObject);
        }
    }
}