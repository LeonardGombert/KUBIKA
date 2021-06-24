using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Carry : MonoBehaviour
    {
        // referenced in the input manager
        // when the player moves a cube, check above to see if there are any cubes
        // if there are, then try to move the above cubes in the same direction

        [SerializeField] private Grid_Kubo kuboGrid;

        public MoveDirection moveDirection;
        private Vector3Int baseCubeCoordinates;

        public void TryMovingStack(Vector3Int cubeCoordinates)
        {
            baseCubeCoordinates = cubeCoordinates;
            if (IsCarryingCube())
            {
            }
        }

        // TODO : needs to be called recursively
        public bool IsCarryingCube()
        {
            // check to see if there is a cube above the moving cube
            kuboGrid.grid.TryGetValue(baseCubeCoordinates + Vector3Int.up, out var targetNode);
            if (targetNode == null) return false;
            return ((CubeBehaviors) targetNode.cubeType != CubeBehaviors.None);
        }
    }
}