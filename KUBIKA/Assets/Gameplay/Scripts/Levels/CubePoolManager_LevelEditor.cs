using UnityEngine;

public class CubePoolManager_LevelEditor : CubePoolManager
{
    [SerializeField] private Grid_LevelEditor _gridLevelEditor;

    public override void AssembleLevel(Node[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].CubeType == ComplexCubeType.None) continue;

            // hell yes
            var newCube = _cubeFactories[(CubeBehaviors) grid[i].CubeType].SpawnCube();
            newCube.transform.position = grid[i].Position;
            newCube.transform.parent = _gridLevelEditor.transform;
            
            var cubeObject = newCube.GetComponent<AbstractCubeObject>();
            cubeObject.Coords = grid[i].Coords;
            
            _gridLevelEditor.placedCubes.Add(cubeObject);
        }
    }
}