using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractGrid : SerializedMonoBehaviour
{
    [ShowInInspector, ReadOnly, TableList] public static KuboState State;
    [SerializeField, ReadOnly, TableList] public Node[] _nodes { get; protected set; }
    public abstract void GenerateNodes();
    public virtual void ClearNodes() => _nodes = null;
    public virtual void GridEquals(Node[] newGrid) => _nodes = newGrid;
}