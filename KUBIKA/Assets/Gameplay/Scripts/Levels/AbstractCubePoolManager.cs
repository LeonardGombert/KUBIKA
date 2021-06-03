using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractCubePoolManager : SerializedMonoBehaviour
{
    /// <summary>
    /// Dictionary of Cube Factories, indexed by the CubeType that they output. By simply declaring the
    /// CubeType that you wish to construct, the Dictionary will automatically use the relevant factory.
    /// </summary>
    [SerializeField] protected Dictionary<CubeBehaviors, AbstractCubeFactory>
        _cubeFactories = new Dictionary<CubeBehaviors, AbstractCubeFactory>();
    
    // Pass the grid that needs to be constructed. 
    public abstract void AssembleLevel(Node[,,] grid);
}