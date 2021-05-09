public class CubePoolManager_Game : AbstractCubePoolManager
{
    public override void AssembleLevel(Node[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].CubeType == ComplexCubeType.None) continue;

            // hell yes
            var newCube = _cubeFactories[(CubeBehaviors) grid[i].CubeType].SpawnCube();
            
            var cubeObject = newCube.GetComponent<CubeObject_Game>();
            
            cubeObject.Coords = grid[i].Coords;
            cubeObject.transform.position = grid[i].Position;
            cubeObject.transform.parent = _gridLevelEditor.transform;
            
            _gridLevelEditor.placedCubes.Add(cubeObject);
        }
    }
}