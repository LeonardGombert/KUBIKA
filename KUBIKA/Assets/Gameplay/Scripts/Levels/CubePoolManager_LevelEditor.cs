using UnityEngine;

public class CubePoolManager_LevelEditor : AbstractCubePoolManager
{
    [SerializeField] private Grid_LevelEditor _gridLevelEditor;

    public override void AssembleLevel(Node[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].CubeTypeType == ComplexCubeType.None) continue;

            // hell yes
            var newCube = _cubeFactories[(CubeBehaviors) grid[i].CubeTypeType].SpawnCube();
            
            var cubeObject = newCube.GetComponent<CubeObject_LevelEditor>();
            
            cubeObject.Coords = grid[i].Coords;
            cubeObject.transform.position = grid[i].Position;
            cubeObject.transform.parent = _gridLevelEditor.transform;
            
            _gridLevelEditor.placedCubes.Add(cubeObject);
        }
    }
}