using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractCubeBehavior : SerializedMonoBehaviour
{    
    public virtual void InitBehavior() {}
    public virtual void PerformBehavior(){}
    public virtual void ResetBehavior() {}
}