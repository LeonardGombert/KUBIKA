using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePoolManager : MonoBehaviour
{
    [SerializeField] AbstractCubeFactory[] _cubeFactories;

    [SerializeField]  GameObject cubePrefab;
    
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _cubeFactories[0].ConfigCube(cubePrefab);
        }
    }

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
