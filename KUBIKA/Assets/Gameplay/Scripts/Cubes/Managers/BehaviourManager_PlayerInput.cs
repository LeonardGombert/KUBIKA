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

        private float swipeTolerance = 10;
        private Vector2 currtouchPosition;
        private Vector2 startTouchPosition;
        private bool _pointerTap;

        private CubeBehavior_Movement targetCubeMovement;

        private void Start()
        {
            mainCamera = Camera.current;
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
            if ((swipeTolerance * swipeTolerance) <= (currtouchPosition - startTouchPosition).sqrMagnitude)
            {
                movementManager.ConvertSwipeToMoveDirection((currtouchPosition - startTouchPosition).normalized);
                movementManager.TryMovingCubeInSwipeDirection();
            }
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
                    movementManager.targetCubeMovement =
                        targetCubeMovement = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                }
            }

            if (context.canceled)
            {
                _pointerTap = false;
            }
        }
    }
}