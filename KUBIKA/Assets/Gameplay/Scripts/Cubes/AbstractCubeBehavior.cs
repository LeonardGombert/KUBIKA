using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCubeBehavior : MonoBehaviour
{    
    public virtual void InitBehavior() {}
    public virtual void PerformBehavior(){}
    public virtual void ResetBehavior() {}
}