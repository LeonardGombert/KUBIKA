using UnityEngine;

public class CubePoolManager_LevelEditor : AbstractCubePoolManager
{
    [SerializeField] private Grid_LevelEditor gridLevelEditor;

    public override void AssembleLevel(Node[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].CubeTypeType == ComplexCubeType.None) continue;

            // hell yes
            var newCube = _cubeFactories[(CubeBehaviors) grid[i].CubeTypeType].SpawnCube();

            var cubeObject = newCube.GetComponent<CubeObject_LevelEditor>();

            cubeObject.ConfigCube(grid[i].Coords, grid[i].CubeTypeType, grid[i].Position, gridLevelEditor.transform);

            gridLevelEditor.placedCubes.Add(cubeObject);
        }
    }
}