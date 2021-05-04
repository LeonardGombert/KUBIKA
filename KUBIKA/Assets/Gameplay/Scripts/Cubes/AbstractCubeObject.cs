using Sirenix.OdinInspector;
using UnityEngine;

public class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] protected GridCoord _index = GridCoord.Zero;
    [SerializeField] protected ComplexCubeType _type;
    [SerializeField] protected AbstractCubeBehavior[] cubeBehaviors;
    
    public GridCoord Index { get => _index; }
    public ComplexCubeType Type { get => _type; }
}