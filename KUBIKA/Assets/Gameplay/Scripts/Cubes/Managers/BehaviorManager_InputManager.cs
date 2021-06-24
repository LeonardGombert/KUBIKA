using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Scripts.Cubes.Managers
{
    // player input
    public class BehaviorManager_InputManager :AbstractBehaviorManager<CubeBehavior_Movement>
    {
        // hold reference to touched cube
        // convert player input into a direction
        // send direction info to the targetCube
        [Header("Player Input")] [SerializeField]
        private Camera mainCamera;

        [SerializeField] private float swipeTolerance;
        [SerializeField] private Grid_Kubo kuboGrid;

        private CubeBehavior_Movement targetCubeMovement;
        private CubeBehaviour_Base targetCubeBase;

        private Vector2 currtouchPosition;
        private Vector2 startTouchPosition;
        private MoveDirection moveDirection;

        private bool _pointerTap;
        private Vector2 _swipeDirection;
        private float _swipeDirX, _swipeDirY;
        private Node targetNode;
        private Vector3Int targetCoordinates;

        // constant update
        public void GetTouchPositionOnScreen(InputAction.CallbackContext context)
        {
            currtouchPosition = context.action.ReadValue<Vector2>();

            if (!_pointerTap) return;
            if (!targetCubeMovement) return;

            CheckIfPlayerSwiping();
        }

        private void CheckIfPlayerSwiping()
        {
            if ((swipeTolerance * swipeTolerance) <= (currtouchPosition - startTouchPosition).sqrMagnitude)
            {
                ConvertSwipeToMoveDirection();
                TryMovingCubeInSwipeDirection();
            }
        }

        private void ConvertSwipeToMoveDirection()
        {
            _swipeDirection = (currtouchPosition - startTouchPosition).normalized;
            _swipeDirX = Mathf.Sign(_swipeDirection.x);
            _swipeDirY = Mathf.Sign(_swipeDirection.y);

            // forward
            if (_swipeDirX >= 0 && _swipeDirY <= 0)
            {
                moveDirection = MoveDirection.Forward;
                return;
            }

            // right
            if (_swipeDirX <= 0 && _swipeDirY <= 0)
            {
                moveDirection = MoveDirection.Right;
                return;
            }

            // back
            if (_swipeDirX <= 0 && _swipeDirY >= 0)
            {
                moveDirection = MoveDirection.Back;
                return;
            }

            // left
            moveDirection = MoveDirection.Left;
        }

        private void TryMovingCubeInSwipeDirection()
        {
            if (CheckIfTargetIsOpen())
            {
                kuboGrid.grid[targetCubeBase.currCoordinates].cubeType = ComplexCubeType.None;
                targetCubeMovement.PerformBehavior(ref targetNode);
            }
            targetCubeMovement = null;
        }

        private bool CheckIfTargetIsOpen()
        {
            targetCoordinates = targetCubeBase.currCoordinates;

            switch (moveDirection)
            {
                case MoveDirection.Forward :
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
            
            // check to see if target position is occupied in the Grid
            kuboGrid.grid.TryGetValue(targetCoordinates, out targetNode);
            
            // if the targeted node doesn't exist
            if (targetNode == null) return false;
            
            return ((CubeBehaviors) targetNode.cubeType) == CubeBehaviors.None;
        }

        // click/tap
        public void TryGetCubeAtTouchPosition(InputAction.CallbackContext context)
        {
            // on finger down
            if (context.started)
            {
                startTouchPosition = currtouchPosition;
                _pointerTap = true;

                var ray = mainCamera.ScreenPointToRay(currtouchPosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
                {
                    targetCubeMovement = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                    targetCubeBase = hitInfo.collider.GetComponent<CubeBehaviour_Base>();
                }
            }

            // on finger up
            if (context.canceled)
            {
                _pointerTap = false;
            }
        }
    }
}