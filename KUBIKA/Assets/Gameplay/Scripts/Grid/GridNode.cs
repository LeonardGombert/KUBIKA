using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    [ShowInInspector, ReadOnly] Dictionary<KuboVector, CubeType> indexes = new Dictionary<KuboVector, CubeType>();
    
    public GridNode(int _x, int _y, int _z, CubeType _currentCube)
    {
        indexes.Add(new KuboVector(_x, _y, _z), _currentCube);
    }

    private KuboVector ConfigKuboIndexes(int _x, int _y, int _z)
    {
        return new KuboVector(_x, _y, _z);
    }
}