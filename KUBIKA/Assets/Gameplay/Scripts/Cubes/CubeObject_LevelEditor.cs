using UnityEngine;

public class CubeObject_LevelEditor : AbstractCubeObject
{
    // config cube function overload for Level Editor
    public void ConfigCube(TriCoords index, ComplexCubeType type)
    {
        gridPosition = index;
        base.cubeType = type;
    }
}