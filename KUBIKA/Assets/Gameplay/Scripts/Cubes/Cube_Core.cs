using Sirenix.OdinInspector;
using UnityEngine;

public class Cube_Core : MonoBehaviour
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

    public void OnSpawn(params CubeBehavior[] list) => cubehaviors = list;

    void OnDestroyed()
    {
    }

    public void ResetCube()
    {
        for (int i = 0; i < cubehaviors.Length; i++)
        {
            cubehaviors[i].ResetBehavior();
        }
    }
}