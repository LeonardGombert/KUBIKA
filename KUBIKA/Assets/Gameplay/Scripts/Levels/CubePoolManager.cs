using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubePoolManager : SerializedMonoBehaviour
{
/// <summary>
/// Dictionary of Cube Factories, indexed by the CubeType that they output. By simply declaring the
/// CubeType that you wish to construct, the Dictionary will automatically use the relevant factory.
/// </summary>
    [SerializeField] Dictionary<CubeBehaviors, Abstract_CubeFactory> _cubeFactories = new Dictionary<CubeBehaviors, Abstract_CubeFactory>();
    
    private List<AbstractCubeObject> allCubes;
    
    private List<AbstractCubeBehavior> baseCubes;
    private List<AbstractCubeBehavior> moveableCubes;
    private List<AbstractCubeBehavior> victoryCubes;
    private List<AbstractCubeBehavior> deliveryCubes;
    private List<AbstractCubeBehavior> elevatorCubes;
    private List<AbstractCubeBehavior> heavyCubes;
    private List<AbstractCubeBehavior> heavyVictoryCubes;
    private List<AbstractCubeBehavior> mineCubes;
    private List<AbstractCubeBehavior> mineVictoryCubes;
    private List<AbstractCubeBehavior> counterCubes;
    private List<AbstractCubeBehavior> switcherCubes;
    private List<AbstractCubeBehavior> switcherVictoryCubes;
    private List<AbstractCubeBehavior> rotatorCubes;

    public void AssembleLevel()
    {
    }

    void ResetAllCubes()
    {
    }
}
