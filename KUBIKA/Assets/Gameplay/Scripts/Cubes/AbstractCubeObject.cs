using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractCubeObject : MonoBehaviour
{
    [SerializeField, ReadOnly] private KuboVector _index = KuboVector.Zero;
    [SerializeField] private CubeType _type;
    [SerializeField] private AbstractCubeBehavior[] cubeBehaviors;
    
    public KuboVector Index { get => _index; set=>_index = value; }
    public CubeType Type { get => _type;  private set => _type = value; }
}