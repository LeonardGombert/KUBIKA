using System.Collections;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;
using UnityEngine;
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

        [SerializeField, ReadOnly] private CubeBehavior_Movement targetCubeMovement;

        private bool canSwipe = true;
        [SerializeField] private CameraRotation cameraRotation;
        private bool movingCamera;

        private KUBIKAInputActions kubikaInput;

        #region Input Setup

        private void Awake() => SetupInputSystem();

        private void SetupInputSystem()
        {
            kubikaInput = new KUBIKAInputActions();

            kubikaInput.Player.TouchScreen.performed += ctx => TryToTouchMovementCube();
            kubikaInput.Player.TouchScreen.canceled += ctx => ClearCachedCube();

            kubikaInput.Player.SwipeScreen.performed += SwipingScreen;

            kubikaInput.Player.RotateCamera.performed += MovingCamera;

            kubikaInput.Player.HoldScreen.performed += ctx => HoldScreenCameraWarmup();
            kubikaInput.Player.HoldScreen.canceled += ctx => StopMovingCamera();
        }

        private void OnEnable()
        {
            kubikaInput.Enable();
        }

        private void OnDisable()
        {
            kubikaInput.Disable();
        }

        #endregion

        #region Input Callbacks

        private void TryToTouchMovementCube()
        {
            startTouchPosition = currtouchPosition;

            var ray = mainCamera.ScreenPointToRay(currtouchPosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
            {
                targetCubeMovement = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
            }
        }

        private void ClearCachedCube()
        {
            targetCubeMovement = null;
        }

        private void SwipingScreen(InputAction.CallbackContext context)
        {
            currtouchPosition = context.action.ReadValue<Vector2>();

            if (targetCubeMovement != null && canSwipe)
            {
                StartCoroutine(CheckIfPlayerSwiping());
            }
        }

        private void HoldScreenCameraWarmup()
        {
            if (targetCubeMovement == null)
            {
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                movingCamera = true;
            }
        }

        private void StopMovingCamera()
        {
            movingCamera = false;
        }

        private Vector2 axis; 
        private void MovingCamera(InputAction.CallbackContext context)
        {
            axis = context.ReadValue<Vector2>();
            if (movingCamera)
            {
            }
        }

        private void Update()
        {
            if (movingCamera)
            {
                _swipeDirection = (currtouchPosition - startTouchPosition).normalized;
                _swipeDirX = Mathf.Sign(_swipeDirection.x);
                
                if (_swipeDirX >= 0)
                {
                    cameraRotation.MoveRight();
                }

                if (_swipeDirX <= 0)
                {
                    cameraRotation.MoveLeft();
                }
            }
        }

        #endregion

        #region Helper Functions

        private IEnumerator CheckIfPlayerSwiping()
        {
            if ((currtouchPosition - startTouchPosition).sqrMagnitude >= (swipeTolerance * swipeTolerance))
            {
                canSwipe = false;
                movementManager.TryMovingCubeInSwipeDirection(ref targetCubeMovement);
                startTouchPosition = currtouchPosition;

                yield return null;
                canSwipe = true;
                // TODO : cubes moved event to check delivery cubes
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

        #endregion
    }
}