using System.Collections.Generic;
using Sirenix.OdinInspector;

public abstract class AbstractBehaviorManager : SerializedMonoBehaviour
{
    /// <summary>
    /// BehaviorManagers have a single dictionary that references all behaviors of its type. They
    /// are indexed by their coordinates, and automatically updated.
    /// </summary>
    protected Dictionary<GridCoord, AbstractCubeBehavior> CubeBehaviors =
        new Dictionary<GridCoord, AbstractCubeBehavior>();

    public virtual void AddBehaviour(GridCoord coordinates, AbstractCubeBehavior behavior) =>
        CubeBehaviors.Add(coordinates, behavior);
}