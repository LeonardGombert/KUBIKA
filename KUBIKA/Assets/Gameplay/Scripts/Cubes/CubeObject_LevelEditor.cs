public class CubeObject_LevelEditor : AbstractCubeObject
{
    public TriCoords Coords
    {
        get => gridPosition;
        set => gridPosition = value;
    }

    public void ConfigCube (TriCoords index, ComplexCubeType type)
    {
        gridPosition = index;
        base.cubeType = type;
    }
}