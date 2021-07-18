using System;
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

        private Vector3 currtouchPosition;
        private Vector3 startTouchPosition;
        private Vector2 raycastTargetPosition;

        [SerializeField, ReadOnly] private CubeBehavior_Movement targetCubeMovement;

        private bool canSwipe = true;
        [SerializeField] private CameraRotation kuboStageRotation;
        private bool movingCamera;

        private KUBIKAInputActions kubikaInput;

        private Vector3 cameraInitPosition;

        #region Input Setup

        private void Awake() => SetupInputSystem();

        private void Start()
        {
            cameraInitPosition = mainCamera.transform.position;
        }

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

            var ray = mainCamera.ScreenPointToRay(raycastTargetPosition);

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
            raycastTargetPosition = context.action.ReadValue<Vector2>();
            currtouchPosition = mainCamera.ScreenToWorldPoint(raycastTargetPosition);

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
        private float dotProduct;

        private void MovingCamera(InputAction.CallbackContext context)
        {
            axis = context.ReadValue<Vector2>();
            if (movingCamera)
            {
            }
        }

        private void Update()
        {
            dotProduct = Vector3.Dot(new Vector3(cameraInitPosition.x, 0, cameraInitPosition.z).normalized,
                new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.z).normalized);
            Debug.Log(dotProduct);

            if (movingCamera)
            {
                _swipeDirection = (currtouchPosition - startTouchPosition).normalized;
                _swipeDirX = Mathf.Sign(_swipeDirection.x);

                if (_swipeDirX >= 0)
                {
                    kuboStageRotation.MoveRight();
                }

                if (_swipeDirX <= 0)
                {
                    kuboStageRotation.MoveLeft();
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
                movementManager.TryMovingCubeInSwipeDirection(targetCubeMovement);
                startTouchPosition = currtouchPosition;

                yield return null;
                canSwipe = true;
                // TODO : cubes moved event to check delivery cubes
            }
        }

        public MoveDirection CalculateMoveDirection()
        {
            _swipeDirection = (currtouchPosition - startTouchPosition).normalized;
            _swipeDirX = Mathf.Sign(_swipeDirection.x) * -Mathf.Sign(mainCamera.transform.position.z);
            _swipeDirY = Mathf.Sign(_swipeDirection.y) * Mathf.Sign(mainCamera.transform.position.z);

            if (dotProduct <= .9f)
            {
                // if on the right side
                if (Mathf.Sign(mainCamera.transform.position.z) > 0)
                {
                    // if signs are the same
                    if (_swipeDirX > 0 && _swipeDirY > 0 || _swipeDirX < 0 && _swipeDirY < 0)
                    {
                        _swipeDirX *= -1;
                    }
                    // if signs are different
                    else
                    {
                        _swipeDirY *= -1;
                    }
                }
                // if on the left side
                else if (Mathf.Sign(mainCamera.transform.position.z) < 0)
                {
                    // if signs are the same
                    if (_swipeDirX > 0 && _swipeDirY > 0 || _swipeDirX < 0 && _swipeDirY < 0)
                    {
                        _swipeDirY *= -1;
                    }
                    // if signs are different
                    else
                    {
                        _swipeDirX *= -1;
                    }
                }
            }

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