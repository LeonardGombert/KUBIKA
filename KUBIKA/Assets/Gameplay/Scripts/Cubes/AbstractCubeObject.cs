using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] protected TriCoords gridPosition = TriCoords.Zero;
    [SerializeField] protected ComplexCubeType cubeType;
    [SerializeField] protected AbstractCubeBehavior[] cubeBehaviors;

    public TriCoords Coords
    {
        get => gridPosition;
        set => gridPosition = value;
    }

    public ComplexCubeType Type
    {
        get => cubeType;
    }

    public void ResetBehaviours()
    {
        for (int i = 0; i < cubeBehaviors.Length; i++)
        {
            cubeBehaviors[i].ResetBehavior();
        }
    }
}