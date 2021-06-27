using System;
using Gameplay.Scripts.Cubes.Managers;
using UnityEngine;

public class BehaviorManager_Delivery : AbstractBehaviorManager<CubeBehavior_Delivery>
{
    private int victoryCount;
    
    private void Start()
    {
        ReferenceProvider.Instance.PlayerInput.PlayerInput += DeliveryCubesCheck;
    }

    private void DeliveryCubesCheck(object sender, EventArgs e)
    {
        Debug.LogWarning("Delivery Cubes are checking.");
        victoryCount = 0;
        
        for (int i = 0; i < managedCubes.Count; i++)
        {
            if (managedCubes[i].bHasAVictoryCube())
            {
                victoryCount++;
            }
        }

        if (victoryCount >= managedCubes.Count)
        {
            Debug.LogWarning("All " + victoryCount + " Victory cubes have been delivered !");
        }
    }
}