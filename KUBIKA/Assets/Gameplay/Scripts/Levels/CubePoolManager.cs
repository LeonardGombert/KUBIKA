using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CubePoolManager : SerializedMonoBehaviour
{
    /// <summary>
    /// Dictionary of Cube Factories, indexed by the CubeType that they output. By simply declaring the
    /// CubeType that you wish to construct, the Dictionary will automatically use the relevant factory.
    /// </summary>
    [SerializeField] protected Dictionary<CubeBehaviors, AbstractCubeFactory>
        _cubeFactories = new Dictionary<CubeBehaviors, AbstractCubeFactory>();
    
    // pass a grid of nodes
    // check if the current cube that needs to be built is in the pool
    // if there is one available, mark it as used

    public abstract void AssembleLevel(Node[] grid);
}