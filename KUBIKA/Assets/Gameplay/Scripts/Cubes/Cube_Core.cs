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

    [SerializeField] private CubeBehavior[] cubeBehaviors;
    
    void OnDestroyed()
    {
    }

    public void ResetCube()
    {
        for (int i = 0; i < cubeBehaviors.Length; i++)
        {
            cubeBehaviors[i].ResetBehavior();
        }
    }
}