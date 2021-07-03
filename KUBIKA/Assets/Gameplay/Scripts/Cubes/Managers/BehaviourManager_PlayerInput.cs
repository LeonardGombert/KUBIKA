using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviourManager_PlayerInput : MonoBehaviour
    {
        [SerializeField] BehaviorManager_Movement movementManager;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private float swipeTolerance = 120;
        private Vector2 _swipeDirection;
        private float _swipeDirX, _swipeDirY;

        private Vector2 currtouchPosition;
        private Vector2 startTouchPosition;

        private CubeBehavior_Movement targetCubeMovement;
        public UnityEvent cubesMoved;

        public void GetTouchPositionOnScreen(InputAction.CallbackContext context)
        {
            currtouchPosition = context.action.ReadValue<Vector2>();

            if (!targetCubeMovement) return;

            StartCoroutine(CheckIfPlayerSwiping());
        }

        private IEnumerator CheckIfPlayerSwiping()
        {
            if ((currtouchPosition - startTouchPosition).sqrMagnitude >= (swipeTolerance * swipeTolerance))
            {
                movementManager.TryMovingCubeInSwipeDirection(ref targetCubeMovement);
                startTouchPosition = currtouchPosition;
                yield return null;
                cubesMoved.Invoke();
            }
        }

        public MoveDirection CalculateMoveDirection()
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
                
                var ray = mainCamera.ScreenPointToRay(currtouchPosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
                {
                    targetCubeMovement = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                }
            }

            if (context.canceled)
            {
                targetCubeMovement = null;
            }
        }
    }
}