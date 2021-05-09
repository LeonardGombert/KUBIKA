using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MoveManager_DictionaryTest : SerializedMonoBehaviour
{
    [ShowInInspector] Dictionary<TriCoords, CubeBehavior_Movement> movementCubes = new Dictionary<TriCoords, CubeBehavior_Movement>();
    public AbstractCubeObject cubes;
    public AbstractCubeObject cubes2;
    private void Awake()
    {
        movementCubes.Add(cubes.Coords, cubes.GetComponent<CubeBehavior_Movement>());
        movementCubes.Add(cubes2.Coords, cubes2.GetComponent<CubeBehavior_Movement>());
    }
}
