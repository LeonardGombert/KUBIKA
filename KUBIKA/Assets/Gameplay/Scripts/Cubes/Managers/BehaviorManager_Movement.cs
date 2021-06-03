using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Scripts.Cubes.Managers
{
    // player input
    public partial class BehaviorManager_Movement : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        // hold reference to touched cube
        // convert player input into a direction
        // send direction info to the targetCube

        [SerializeField] private CubeBehavior_Movement _playerTarget;
        [SerializeField] private Vector2 _pointerPosition;

        public void UpdatePointerPos(InputAction.CallbackContext context) =>
            _pointerPosition = context.action.ReadValue<Vector2>();

        public void TryGetCube(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Ray ray = Camera.main.ScreenPointToRay(_pointerPosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
                {
                    _playerTarget = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                }
            }
        }
    }

    // cubes pushing each other
    public partial class BehaviorManager_Movement
    {
        //  
    }

    // cubes carrying other cubes
    public partial class BehaviorManager_Movement
    {
        // 
    }

    // gravity
    public partial class BehaviorManager_Movement
    {
        // 
    }
}