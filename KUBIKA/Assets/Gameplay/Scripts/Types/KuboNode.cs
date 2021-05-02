using Sirenix.OdinInspector;

public class KuboNode
{
    [ShowInInspector, ReadOnly] KuboVector[] indexes = new KuboVector[3];
    [ShowInInspector, ReadOnly] CubeType currentCube;
    
    public KuboNode(int _x, int _y, int _z, CubeType _currentCube)
    {
        indexes[0] = new KuboVector(_x, _y, _z); // position 1
        indexes[1] = new KuboVector(_z, _x, _y); // position 2
        indexes[2] = new KuboVector(_y, _z, _x); // position 3

        currentCube = _currentCube;
    }
}