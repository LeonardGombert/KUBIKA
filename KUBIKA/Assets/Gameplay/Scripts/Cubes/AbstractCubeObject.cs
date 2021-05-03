using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] protected KuboVector _index = KuboVector.Zero;
    [SerializeField] protected CubeType _type;
    [SerializeField] protected AbstractCubeBehavior[] cubeBehaviors;
    
    public KuboVector Index { get => _index; }
    public CubeType Type { get => _type; }
}