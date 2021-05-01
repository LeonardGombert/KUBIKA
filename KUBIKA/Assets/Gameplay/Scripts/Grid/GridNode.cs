using System;
using Sirenix.OdinInspector;

[System.Serializable]
public class GridNode
{
    [ShowInInspector, ReadOnly] KuboVector[] indexes = new KuboVector[3];
    
    public GridNode(int _x, int _y, int _z, CubeType _currentCube)
    {
        indexes[0] = new KuboVector(_x, _y, _z); // position 1
        indexes[1] = new KuboVector(_z, _x, _y); // position 2
        indexes[2] = new KuboVector(_y, _z, _x); // position 3
    }

    private KuboVector ConfigKuboIndexes(int _x, int _y, int _z)
    {
        return new KuboVector(_x, _y, _z);
    }
}