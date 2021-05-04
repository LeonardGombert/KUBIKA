using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MoveManager_DictionaryTest : SerializedMonoBehaviour
{
    [ShowInInspector] Dictionary<GridCoord, CubeBehavior_Movement> movementCubes = new Dictionary<GridCoord, CubeBehavior_Movement>();
    public AbstractCubeObject cubes;
    public AbstractCubeObject cubes2;
    private void Awake()
    {
        movementCubes.Add(cubes.Index, cubes.GetComponent<CubeBehavior_Movement>());
        movementCubes.Add(cubes2.Index, cubes2.GetComponent<CubeBehavior_Movement>());
    }
}
