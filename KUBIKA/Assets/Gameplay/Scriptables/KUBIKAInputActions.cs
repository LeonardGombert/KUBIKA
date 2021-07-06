// GENERATED AUTOMATICALLY FROM 'Assets/Gameplay/Scriptables/KUBIKAInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KUBIKAInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KUBIKAInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""KUBIKAInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2b45168a-7735-4b16-8485-98cc67495dbb"",
            ""actions"": [
                {
                    ""name"": ""TouchScreen"",
                    ""type"": ""Button"",
                    ""id"": ""d5a236da-3a5b-4728-9ba7-dd1590d3c7f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwipeScreen"",
                    ""type"": ""Value"",
                    ""id"": ""b4a7f368-efcf-4ccb-96bc-796cda4b2745"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HoldScreen"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d7ea33c5-5232-49f5-b65a-0b0b3c87ca4c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""16e5953c-cb41-4f0c-8fa7-d3418edf2cf3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9a2e5a76-1b09-4ac0-b4cb-c83ecdc3163f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Composite;Keyboard&Mouse"",
                    ""action"": ""SwipeScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4e91fe9-6ded-4d29-921f-18d992c3450e"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch;Composite"",
                    ""action"": ""SwipeScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2124d13f-5837-4450-9ee0-b30965ad26fb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Composite"",
                    ""action"": ""HoldScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ca84b82-ca0c-4385-90c6-e30c7d76a3d9"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch;Composite"",
                    ""action"": ""HoldScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a437b9fe-af23-4a6f-9d9d-30d6954cca78"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Composite"",
                    ""action"": ""TouchScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1aa01c4-fc44-40b8-b8a6-77e770a57007"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch;Composite"",
                    ""action"": ""TouchScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""764947f7-7c82-4fe4-b2a3-d863f6a793c3"",
                    ""path"": ""<Touchscreen>/primaryTouch/startPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch;Composite"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Composite"",
            ""bindingGroup"": ""Composite"",
            ""devices"": [
                {
                    ""devicePath"": ""<Pointer>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_TouchScreen = m_Player.FindAction("TouchScreen", throwIfNotFound: true);
        m_Player_SwipeScreen = m_Player.FindAction("SwipeScreen", throwIfNotFound: true);
        m_Player_HoldScreen = m_Player.FindAction("HoldScreen", throwIfNotFound: true);
        m_Player_RotateCamera = m_Player.FindAction("RotateCamera", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_TouchScreen;
    private readonly InputAction m_Player_SwipeScreen;
    private readonly InputAction m_Player_HoldScreen;
    private readonly InputAction m_Player_RotateCamera;
    public struct PlayerActions
    {
        private @KUBIKAInputActions m_Wrapper;
        public PlayerActions(@KUBIKAInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchScreen => m_Wrapper.m_Player_TouchScreen;
        public InputAction @SwipeScreen => m_Wrapper.m_Player_SwipeScreen;
        public InputAction @HoldScreen => m_Wrapper.m_Player_HoldScreen;
        public InputAction @RotateCamera => m_Wrapper.m_Player_RotateCamera;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @TouchScreen.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchScreen;
                @TouchScreen.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchScreen;
                @TouchScreen.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchScreen;
                @SwipeScreen.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwipeScreen;
                @SwipeScreen.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwipeScreen;
                @SwipeScreen.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwipeScreen;
                @HoldScreen.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldScreen;
                @HoldScreen.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldScreen;
                @HoldScreen.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldScreen;
                @RotateCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCamera;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchScreen.started += instance.OnTouchScreen;
                @TouchScreen.performed += instance.OnTouchScreen;
                @TouchScreen.canceled += instance.OnTouchScreen;
                @SwipeScreen.started += instance.OnSwipeScreen;
                @SwipeScreen.performed += instance.OnSwipeScreen;
                @SwipeScreen.canceled += instance.OnSwipeScreen;
                @HoldScreen.started += instance.OnHoldScreen;
                @HoldScreen.performed += instance.OnHoldScreen;
                @HoldScreen.canceled += instance.OnHoldScreen;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_CompositeSchemeIndex = -1;
    public InputControlScheme CompositeScheme
    {
        get
        {
            if (m_CompositeSchemeIndex == -1) m_CompositeSchemeIndex = asset.FindControlSchemeIndex("Composite");
            return asset.controlSchemes[m_CompositeSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnTouchScreen(InputAction.CallbackContext context);
        void OnSwipeScreen(InputAction.CallbackContext context);
        void OnHoldScreen(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
    }
}
