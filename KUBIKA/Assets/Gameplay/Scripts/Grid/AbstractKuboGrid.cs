using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractKuboGrid : SerializedMonoBehaviour
{
    [SerializeField, ReadOnly, TableList] private KuboNode[] _gridNodes;
    [ShowInInspector, ReadOnly, TableList] public static KuboState KuboState;

    public KuboNode[] Grid
    {
        get => _gridNodes;
        protected set => _gridNodes = value;
    }
    
    public abstract void BuildGrid();
    public abstract void RotateGrid();
    public abstract void ResetGrid();
}