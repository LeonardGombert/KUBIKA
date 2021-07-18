using Gameplay.Scripts.Cubes.Managers;
using UnityEngine;

public class KuboGravityApplicator : MonoBehaviour
{
     [SerializeField] private Grid_Kubo kuboGrid;
     [SerializeField] private BehaviorManager_Movement movementManager;
    public void MakeFallAfterRotation()
    {
        // recalculate current node coordinates
        var moveableCubes = FindObjectsOfType<CubeBehavior_Movement>();
        
        foreach (var fallingCube in moveableCubes)
        {
            movementManager.MakeBaseCubeFall(fallingCube);
            Debug.Log("iteration");
        }
    }
}