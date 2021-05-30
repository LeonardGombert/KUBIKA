using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] protected TriCoords gridPosition = TriCoords.Zero;
    [SerializeField] protected ComplexCubeType cubeType;
    [SerializeField] protected AbstractCubeBehavior[] cubeBehaviors;

    // public getters for Cube Coords and CubeType. Sets are protected above^.
    public TriCoords Coords => gridPosition;
    public ComplexCubeType CubeType => cubeType;

    public void ConfigCube(TriCoords coords, ComplexCubeType complexCubeType, Vector3 position, Transform parent)
    {
        gridPosition = coords;
        var @ref = transform;
        @ref.position = position;
        @ref.parent = parent;
    }

    public void ResetBehaviours()
    {
        for (int i = 0; i < cubeBehaviors.Length; i++)
        {
            cubeBehaviors[i].ResetBehavior();
        }
    }
}