using Sirenix.OdinInspector;

public abstract class AbstractGrid : SerializedMonoBehaviour
{
    [ShowInInspector, ReadOnly] public Node[,,] Nodes { get; protected set; }    
    public static float width = 1.2f;

    public abstract void GenerateNodes();
    
    public virtual void ClearNodes() => Nodes = null;
    
    /// <summary>
    /// A method used to set the value of the nodes array upon loading a level.
    /// </summary>
    /// <param name="newGrid">The grid that has been loaded from a level file</param>
    public void AssignLoadedGrid(Node[,,] newGrid) => Nodes = newGrid;
}