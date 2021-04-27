using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCubeFactory : MonoBehaviour
{
    protected CubeCore finalCube;
    public abstract GameObject ConfigCube(GameObject cubePrefab);
    public virtual void PositionCube(int index) =>  finalCube._index = index;
}