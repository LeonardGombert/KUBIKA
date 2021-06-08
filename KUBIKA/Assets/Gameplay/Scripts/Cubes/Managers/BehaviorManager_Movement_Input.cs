using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Scripts.Cubes.Managers
{
    // player input
    public partial class BehaviorManager_Movement
    {
        // hold reference to touched cube
        // convert player input into a direction
        // send direction info to the targetCube

        [Header("Player Input")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float swipeTolerance;
        
        [Header("Input Debugging")]
        [SerializeField, ReadOnly] private CubeBehavior_Movement playerTarget;
        [SerializeField, ReadOnly] private Vector2 pointerPosition;
        [SerializeField, ReadOnly] private Vector2 pointerTapPosition;
        [SerializeField, ReadOnly] private MoveDirection moveDirection;
        private bool _pointerTap = false;
        private bool _swiping = false;
        private Vector2 _swipeDirection;
        [SerializeField, ReadOnly] private float _swipeDirX, _swipeDirY;

        // constant update
        public void UpdatePointerPos(InputAction.CallbackContext context)
        {
            pointerPosition = context.action.ReadValue<Vector2>();

            if (!_pointerTap) return;

            // if the player isn't in a swiping state
            if (!_swiping)
            {
                // check if he should be
                if (swipeTolerance * swipeTolerance <= (pointerPosition - pointerTapPosition).sqrMagnitude)
                {
                    SwipeToDirection();
                    _swiping = true;
                }
            }

            // else if he is swiping
            if (_swiping)
            {
                playerTarget?.PerformBehavior(moveDirection);
                playerTarget = null;
            }
        }

        private void SwipeToDirection()
        {
            _swipeDirection = (pointerPosition - pointerTapPosition).normalized;
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

        // click/tap
        public void TryGetCube(InputAction.CallbackContext context)
        {

            // on finger down
            if (context.started)
            {
                pointerTapPosition = pointerPosition;
                _pointerTap = true;

                var ray = mainCamera.ScreenPointToRay(pointerPosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
                {
                    playerTarget = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                }
            }

            // on finger up
            if (context.canceled)
            {
                _pointerTap = false;
                _swiping = false;
            }
        }
    }
}