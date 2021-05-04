using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class KUBO : MonoBehaviour
{
    [SerializeField] private AbstractGrid Grid;
    [ShowInInspector, ReadOnly, TableList] public static KuboState State;
}
