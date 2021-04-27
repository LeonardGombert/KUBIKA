using Sirenix.OdinInspector;
using UnityEngine;

public class CubeCore : MonoBehaviour
{
    [ReadOnly] [SerializeField] public int _index;
    public int index
    {
        get { return _index; }
        set
        {
            _index = value;
            // TODO : set transform.position to be position of index in grid
        }
    }

    [ShowInInspector] CubeBehavior[] cubehaviors;

    void OnSpawn()
    {
    }

    void OnDestroyed()
    {
    }
}