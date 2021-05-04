using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractGrid : SerializedMonoBehaviour
{
    [SerializeField, ReadOnly, TableList] public GridNode[] _nodes { get; protected set; }
    public abstract void GenerateNodes();
    public virtual void ClearNodes() => _nodes = null;
}