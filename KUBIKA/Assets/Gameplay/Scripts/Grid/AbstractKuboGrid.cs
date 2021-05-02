using Sirenix.OdinInspector;
using UnityEditorInternal;
using UnityEngine;

public abstract class AbstractKuboGrid : SerializedMonoBehaviour
{
    [SerializeField, ReadOnly, TableList] private KuboNode[] _gridNodes;

    public KuboNode[] grid
    {
        get { return _gridNodes; }
        protected set => _gridNodes = value;
    }

    public abstract void BuildGrid();
    public abstract void RotateGrid();
    public abstract void ResetGrid();
}