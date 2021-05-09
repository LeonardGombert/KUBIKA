public class CubeObject_LevelEditor : AbstractCubeObject
{
    public TriCoords Coords
    {
        get => _coords;
        set => _coords = value;
    }

    public void ConfigCube (TriCoords index, ComplexCubeType type)
    {
        _coords = index;
        _type = type;
    }
}