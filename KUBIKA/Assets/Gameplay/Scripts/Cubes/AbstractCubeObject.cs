using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractCubeObject : MonoBehaviour
{
    [ReadOnly] [SerializeField] private int _index;
    [SerializeField] private CubeType _type;
    [SerializeField] private AbstractCubeBehavior[] cubeBehaviors;
    
    public int Index { get => _index; set=>_index = value; }
    public CubeType Type { get => _type;  private set => _type = value; }
}