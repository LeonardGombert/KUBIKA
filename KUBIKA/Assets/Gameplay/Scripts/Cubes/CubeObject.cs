using Sirenix.OdinInspector;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    [ReadOnly] [SerializeField] private int _index;
    [SerializeField] private CubeType _type;
    [SerializeField] private AbstractCubeBehavior[] cubeBehaviors;
    
    public int Index { get => _index; set=>_index = value; }
    public CubeType Type { get => _type;  private set => _type = value; }

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