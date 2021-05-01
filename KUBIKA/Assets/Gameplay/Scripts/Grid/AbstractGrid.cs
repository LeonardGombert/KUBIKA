using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractGrid : SerializedMonoBehaviour
{
    [ShowInInspector] protected GridNode[] _gridNodes;

    public abstract void BuildGrid();
    public abstract void RotateGrid();
    public abstract void ResetGrid();
}
