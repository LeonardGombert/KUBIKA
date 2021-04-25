using Unity.Collections;
using UnityEngine;

public class CubeBase : MonoBehaviour
{
    private int index;
    [SerializeField] CubeBehaviorsEnum cubeBehaviorsFlag;
    [ReadOnly] [SerializeField] CubeBehavior[] cubehaviors;

    void OnSpawn() { }

    void OnDestroyed() { }
}