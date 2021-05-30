using UnityEngine;

public class CubePoolManager_Game : AbstractCubePoolManager
{
    [SerializeField] private Grid_Kubo gameGrid;

    public override void AssembleLevel(Node[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].CubeTypeType == ComplexCubeType.None) continue;

            // hell yes
            var newCube = _cubeFactories[(CubeBehaviors) grid[i].CubeTypeType].SpawnCube();

            var cubeObject = newCube.GetComponent<CubeObject_Game>();

            cubeObject.ConfigCube(grid[i].Coords, grid[i].CubeTypeType, grid[i].Position, gameGrid.transform);

            //_gameGrid.placedCubes.Add(cubeObject);
        }
    }
}