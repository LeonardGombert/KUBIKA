using Sirenix.OdinInspector;
using UnityEngine;

public class KuboNode
{
    [ShowInInspector, ReadOnly] private KuboVector _indexes;
    [ShowInInspector, ReadOnly] private CubeType _currentCube;
    [ShowInInspector, ReadOnly] private Vector3 _rotation;

    public Vector3 CurrConfig => _indexes.Config[(int)(AbstractKuboGrid.KuboState)];

    public KuboNode(int x, int y, int z, CubeType currentCube)
    {
        _indexes = new KuboVector(x, y, z);
        _currentCube = currentCube;
    }
}