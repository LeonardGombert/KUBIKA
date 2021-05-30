public class CubeObject_Game : AbstractCubeObject
{
    public TriCoords Coords
    {
        get => gridPosition;
        set => gridPosition = value;
    }
}