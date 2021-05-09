using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] protected TriCoords _coords = TriCoords.Zero;
    [SerializeField] protected ComplexCubeType _type;
    [SerializeField] protected AbstractCubeBehavior[] cubeBehaviors;

    public TriCoords Coords
    {
        get => _coords;
        set => _coords = value;
    }

    public ComplexCubeType Type
    {
        get => _type;
    }

    public void ResetBehaviours()
    {
        for (int i = 0; i < cubeBehaviors.Length; i++)
        {
            cubeBehaviors[i].ResetBehavior();
        }
    }
}