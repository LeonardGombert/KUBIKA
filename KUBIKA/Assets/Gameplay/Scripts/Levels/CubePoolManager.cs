using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubePoolManager : SerializedMonoBehaviour
{
    // Use CubeType to access matching Cube Factory
    [SerializeField] Dictionary<CubeType, AbstractCubeFactory> _cubeFactories = new Dictionary<CubeType, AbstractCubeFactory>();
    
    private List<Cube_Core> allCubes;
    
    private List<CubeBehavior> baseCubes;
    private List<CubeBehavior> moveableCubes;
    private List<CubeBehavior> victoryCubes;
    private List<CubeBehavior> deliveryCubes;
    private List<CubeBehavior> elevatorCubes;
    private List<CubeBehavior> heavyCubes;
    private List<CubeBehavior> heavyVictoryCubes;
    private List<CubeBehavior> mineCubes;
    private List<CubeBehavior> mineVictoryCubes;
    private List<CubeBehavior> counterCubes;
    private List<CubeBehavior> switcherCubes;
    private List<CubeBehavior> switcherVictoryCubes;
    private List<CubeBehavior> rotatorCubes;

    public void AssembleLevel()
    {
        
    }

    void ResetAllCubes()
    {
        for (int i = 0; i < allCubes.Count; i++)
        {
            allCubes[i].ResetCube();
        }   
    }
}
