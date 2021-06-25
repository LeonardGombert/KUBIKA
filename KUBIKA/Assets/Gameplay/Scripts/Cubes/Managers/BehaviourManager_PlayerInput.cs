using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviourManager_PlayerInput : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        [SerializeField] BehaviorManager_Movement movementManager;
        [SerializeField] BehaviorManager_Gravity gravityManager;
        [SerializeField] BehaviorManager_Push pushManager;

        private Camera mainCamera;

        private float swipeTolerance = 30;
        private Vector2 _swipeDirection;
        private float _swipeDirX, _swipeDirY;

        private Vector2 currtouchPosition;
        private Vector2 startTouchPosition;
        private bool _pointerTap;

        private CubeBehavior_Movement targetCubeMovement;

        private void Start()
        {
            mainCamera = FindObjectOfType<Camera>();
        }

        public void GetTouchPositionOnScreen(InputAction.CallbackContext context)
        {
            currtouchPosition = context.action.ReadValue<Vector2>();

            if (!_pointerTap) return;
            if (!targetCubeMovement) return;

            CheckIfPlayerSwiping();
        }

        private void CheckIfPlayerSwiping()
        {
            if ((currtouchPosition - startTouchPosition).sqrMagnitude >= (swipeTolerance * swipeTolerance))
            {
                movementManager.TryMovingCubeInSwipeDirection(ConvertSwipeToMoveDirection());
                targetCubeMovement = null;
            }
        }

        private MoveDirection ConvertSwipeToMoveDirection()
        {
            _swipeDirection = (currtouchPosition - startTouchPosition).normalized;
            _swipeDirX = Mathf.Sign(_swipeDirection.x);
            _swipeDirY = Mathf.Sign(_swipeDirection.y);

            if (_swipeDirX >= 0 && _swipeDirY <= 0)
            {
                return MoveDirection.Forward;
            }

            if (_swipeDirX <= 0 && _swipeDirY <= 0)
            {
                return MoveDirection.Right;
            }

            if (_swipeDirX <= 0 && _swipeDirY >= 0)
            {
                return MoveDirection.Back;
            }

            return MoveDirection.Left;
        }

        public void TryGetCubeAtTouchPosition(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                startTouchPosition = currtouchPosition;
                _pointerTap = true;

                var ray = mainCamera.ScreenPointToRay(currtouchPosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
                {
                    targetCubeMovement = movementManager.movingCube =
                        hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                }
            }

            if (context.canceled)
            {
                _pointerTap = false;
            }
        }
    }
}