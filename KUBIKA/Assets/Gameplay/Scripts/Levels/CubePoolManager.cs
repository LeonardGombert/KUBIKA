using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubePoolManager : SerializedMonoBehaviour
{
    /// <summary>
    /// Dictionary of Cube Factories, indexed by the CubeType that they output. By simply declaring the
    /// CubeType that you wish to construct, the Dictionary will automatically use the relevant factory.
    /// </summary>
    [SerializeField] Dictionary<CubeBehaviors, AbstractCubeFactory>
        _cubeFactories = new Dictionary<CubeBehaviors, AbstractCubeFactory>();

    private Dictionary<AbstractCubeObject, bool> allCubes = new Dictionary<AbstractCubeObject, bool>();

    private Dictionary<AbstractCubeObject, bool> baseCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> moveableCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> victoryCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> deliveryCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> elevatorCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> heavyCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> heavyVictoryCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> mineCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> mineVictoryCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> counterCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> switcherCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> switcherVictoryCubes = new Dictionary<AbstractCubeObject, bool>();
    private Dictionary<AbstractCubeObject, bool> rotatorCubes = new Dictionary<AbstractCubeObject, bool>();

    // pass a grid of nodes
    // check if the current cube that needs to be built is in the pool
    // if there is one available, mark it as used

    public void AssembleLevel(GridNode[] requirements)
    {
        for (int i = 0; i < requirements.Length; i++)
        {
            if (requirements[i].CubeType == ComplexCubeType.None) continue;

            // hell yes
            AbstractCubeObject newCube = _cubeFactories[(CubeBehaviors) requirements[i].CubeType]
                .PlaceCube(requirements[i].CurrPos);

            /*switch (requirements[i].CubeType)
            {
                case ComplexCubeType.Static:
                    baseCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.Moveable:
                    moveableCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.MoveableVictory:
                    victoryCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.StaticDelivery:
                    deliveryCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.StaticElevator:
                    elevatorCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.Heavy:
                    heavyCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.VictoryHeavy:
                    heavyVictoryCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.Mine:
                    mineCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.VictoryMine:
                    mineVictoryCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.Counter:
                    counterCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.Switcher:
                    switcherCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.VictorySwitcher:
                    switcherVictoryCubes.Add(newCube, true);
                    break;
                case ComplexCubeType.Rotator:
                    rotatorCubes.Add(newCube, true);
                    break;
            }*/

//            allCubes.Add(newCube, true);
        }
    }
}