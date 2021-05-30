using UnityEngine;

public class CubeObject_Game : AbstractCubeObject
{
    /// <summary>
    /// Returns the Cube's current coordinates relative to the Kubo's rotation.
    /// </summary>
    public Vector3Int CurrCoords => gridPosition.Pos[(int) Grid_Kubo.State];
}