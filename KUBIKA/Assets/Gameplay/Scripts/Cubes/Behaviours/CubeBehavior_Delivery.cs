using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior_Delivery : AbstractCubeBehavior
{
    public override void InitBehavior()
    {
        // throw new System.NotImplementedException();
    }

    public bool bHasAVictoryCube()
    {
        Debug.Log("Raycasting");
        Debug.DrawRay(transform.position, transform.up * 50, Color.green);
        if (Physics.Raycast(transform.position, transform.up, out var hitInfo, 1.25f))
        {
            CubeBehavior_Victory victoryCube = hitInfo.collider.GetComponent<CubeBehavior_Victory>();
            if (victoryCube)
            {
                Debug.Log("Victory cube " + victoryCube.gameObject.name + "has been delivered");
                return true;
            }
        }
        return false;
    }
    
    public override void ResetBehavior()
    {
        throw new System.NotImplementedException();
    }

}