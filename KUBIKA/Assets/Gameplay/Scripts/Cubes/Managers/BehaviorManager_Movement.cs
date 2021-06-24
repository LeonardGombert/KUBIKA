using UnityEngine;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Movement : MonoBehaviour
    {
        [SerializeField] private Grid_Kubo kuboGrid;
        [SerializeField] BehaviorManager_Carry carryManager;
        
        private Vector2 _swipeDirection;
        private float _swipeDirX, _swipeDirY;
        private MoveDirection moveDirection;
        
        public CubeBehavior_Movement targetCubeMovement;
        private Node targetNode;
        private Vector3Int targetCoordinates;

        private Vector3Int cubeCoordinates => targetCubeMovement.cubeBase.currCoordinates;
        
        public void ConvertSwipeToMoveDirection(Vector2 normalizedSwipeDirection)
        {
            _swipeDirection = normalizedSwipeDirection;
            _swipeDirX = Mathf.Sign(_swipeDirection.x);
            _swipeDirY = Mathf.Sign(_swipeDirection.y);

            if (_swipeDirX >= 0 && _swipeDirY <= 0)
            {
                moveDirection = MoveDirection.Forward;
                return;
            }

            if (_swipeDirX <= 0 && _swipeDirY <= 0)
            {
                moveDirection = MoveDirection.Right;
                return;
            }

            if (_swipeDirX <= 0 && _swipeDirY >= 0)
            {
                moveDirection = MoveDirection.Back;
                return;
            }

            carryManager.moveDirection = moveDirection = MoveDirection.Left;
        }

        public void TryMovingCubeInSwipeDirection()
        {
            if (TargetIsOpen() && HasCubeUnder())
            {
                kuboGrid.grid[cubeCoordinates].cubeType = ComplexCubeType.None;
                targetCubeMovement.PerformBehavior(ref targetNode);
                carryManager.TryMovingStack(cubeCoordinates);
            }

            targetCubeMovement = null;
        }

        public bool TargetIsOpen()
        {
            targetCoordinates = cubeCoordinates;

            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    targetCoordinates += Vector3Int.forward;
                    break;
                case MoveDirection.Right:
                    targetCoordinates += Vector3Int.right;
                    break;
                case MoveDirection.Back:
                    targetCoordinates += Vector3Int.back;
                    break;
                case MoveDirection.Left:
                    targetCoordinates += Vector3Int.left;
                    break;
                case MoveDirection.Down:
                    targetCoordinates += Vector3Int.down;
                    break;
            }

            // check to see if target position is open in the Grid
            kuboGrid.grid.TryGetValue(targetCoordinates, out targetNode);
            if (targetNode == null) return false;
            return ((CubeBehaviors) targetNode.cubeType) == CubeBehaviors.None;
        }

        public bool HasCubeUnder()
        {
            // check to see if target position has a cube underneath
            kuboGrid.grid.TryGetValue(targetCoordinates + Vector3Int.down, out var targetNode2);
            if (targetNode2 == null) return false;
            return ((CubeBehaviors) targetNode2.cubeType) != CubeBehaviors.None;
        }
    }
}