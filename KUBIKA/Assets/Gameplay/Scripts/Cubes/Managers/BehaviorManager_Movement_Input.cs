using System;
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

        [Header("Player Input")] [SerializeField]
        private Camera mainCamera;

        [SerializeField] private float swipeTolerance;
        [SerializeField] private Grid_Kubo kuboGrid;

        private CubeBehavior_Movement targetCubeMovement;
        private CubeBehaviour_Base targetCubeBase;

        private Vector2 pointerPosition;
        private Vector2 pointerTapPosition;
        private MoveDirection moveDirection;

        private bool _pointerTap;
        private bool _swiping;
        private Vector2 _swipeDirection;
        private float _swipeDirX, _swipeDirY;

        // constant update
        public void UpdatePointerPos(InputAction.CallbackContext context)
        {
            pointerPosition = context.action.ReadValue<Vector2>();

            if (!_pointerTap) return;
            if (!targetCubeMovement) return;

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
                // if the target position is open
                if (bIsOpen(TargetPositionToCoords())) 
                    targetCubeMovement.PerformBehavior(moveDirection);

                targetCubeMovement = null;
            }
        }

        private bool bIsOpen(TriCoords targetPosition)
        {
            Debug.Log(targetPosition.Pos[0]);
            kuboGrid.nodeDictionary.TryGetValue(targetPosition, out var temp);
            Debug.Log(temp);
            
            // if the target spot is open
            return temp == CubeBehaviors.None;
        }

        private TriCoords TargetPositionToCoords()
        {
            Vector3Int value;

            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    value = Vector3Int.forward;
                    break;
                case MoveDirection.Right:
                    value = Vector3Int.right;
                    break;
                case MoveDirection.Back:
                    value = Vector3Int.back;
                    break;
                case MoveDirection.Left:
                    value = Vector3Int.left;
                    break;
                default:
                    value = Vector3Int.zero;
                    break;
            }
            
            return targetCubeBase.TriCoords + value;
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
                    targetCubeMovement = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                    targetCubeBase = hitInfo.collider.GetComponent<CubeBehaviour_Base>();
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