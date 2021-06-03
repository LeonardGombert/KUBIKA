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

        [SerializeField] private CubeBehavior_Movement playerTarget;
        [SerializeField] private Vector2 pointerPosition;
        [SerializeField] private MoveDirection moveDirection;

        public void UpdatePointerPos(InputAction.CallbackContext context) =>
            pointerPosition = context.action.ReadValue<Vector2>();

        public void TryGetCube(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f))
                {
                    playerTarget = hitInfo.collider.GetComponent<CubeBehavior_Movement>();
                }
            }
        }

        public void TryMoveCube(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                playerTarget.PerformBehavior(moveDirection);
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