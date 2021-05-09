public class CubeObject_LevelEditor : AbstractCubeObject
{
    public void ConfigCube (TriCoords index, ComplexCubeType type)
    {
        _coords = index;
        _type = type;
    }
}