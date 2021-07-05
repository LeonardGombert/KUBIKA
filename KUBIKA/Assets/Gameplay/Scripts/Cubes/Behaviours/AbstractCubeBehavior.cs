using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractCubeBehavior : MonoBehaviour
{
    public virtual void InitBehavior()
    {
    }

    public virtual void PerformBehavior()
    {
    }

    public virtual void ResetBehavior()
    {
    }

    public virtual void UndoLast()
    {
    }
}