using UnityEngine;

public class CubeObject_Game : AbstractCubeObject
{
    [SerializeField] protected AbstractCubeBehavior[] cubeBehaviors;
    
    /// <summary>
    /// Returns the Cube's current coordinates relative to the Kubo's rotation.
    /// </summary>
    public Vector3Int CurrCoords => gridPosition.Pos[(int) Grid_Kubo.State];

    public void ResetBehaviours()
    {
        for (int i = 0; i < cubeBehaviors.Length; i++)
        {
            cubeBehaviors[i].ResetBehavior();
        }
    }
}