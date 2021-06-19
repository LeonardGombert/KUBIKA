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

        private Vector2 pointerPosition;
        private Vector2 pointerTapPosition;
        private MoveDirection moveDirection;

        private bool _pointerTap;
        private bool _swiping;
        private Vector2 _swipeDirection;
        private float _swipeDirX, _swipeDirY;
        private Node targetNode;
        private TriCoords targetPosition;

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
                    GetMoveDirection();
                    _swiping = true;
                }
            }

            // else if he is swiping
            if (_swiping)
            {
                // if the target position is open
                if (bIsOpen())
                    targetCubeMovement.PerformBehavior(targetNode.Position);

                targetCubeMovement = null;
            }
        }

        private bool bIsOpen()
        {
            TriCoords targetPosition = targetCubeBase.TriCoords;

            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    targetPosition += TriCoords.Forward;
                    break;
                case MoveDirection.Right:
                    targetPosition += TriCoords.Right;
                    break;
                case MoveDirection.Back:
                    targetPosition += TriCoords.Back;
                    break;
                case MoveDirection.Left:
                    targetPosition += TriCoords.Left;
                    break;
                case MoveDirection.Down:
                    targetPosition += TriCoords.Down;
                    break;
            }

            Debug.Log("Target Pos is " + targetPosition.Pos[0]);
            Debug.Log("Target Pos is " + targetPosition.Pos[1]);
            Debug.Log("Target Pos is " + targetPosition.Pos[2]);
            Debug.Log("Node Dictionary " + kuboGrid.NodeDictionary[targetPosition].Coords.Pos[0]);
            Debug.Log("Node Dictionary " + kuboGrid.NodeDictionary[targetPosition].Coords.Pos[1]);
            Debug.Log("Node Dictionary " + kuboGrid.NodeDictionary[targetPosition].Coords.Pos[2]);
            // check to see if target position is occupied in the Grid
            kuboGrid.NodeDictionary.TryGetValue(targetPosition, out targetNode);
            return ((CubeBehaviors)targetNode.CubeType) == CubeBehaviors.None;
        }

        private void GetMoveDirection()
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