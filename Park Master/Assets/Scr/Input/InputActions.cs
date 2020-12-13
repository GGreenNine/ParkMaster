// GENERATED AUTOMATICALLY FROM 'Assets/Scr/Input/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""GameControl"",
            ""id"": ""f3957450-fd84-4a20-83bd-3436c693c2e1"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""853ee2f2-0198-4614-9a84-23dab71810aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""fb8c3b13-19f3-478d-98be-d7fe4e29b241"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LeftHold"",
                    ""type"": ""Value"",
                    ""id"": ""58f2c8a9-71e9-4b2a-849f-511334720191"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1,pressPoint=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""36ff2e58-ea46-451d-b1f4-de40aa04dc18"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b29de5ab-68bb-460e-894d-94eb8e2635e9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b4fe6e3-6eb2-4dcc-b735-74eff1a86c63"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GameControl
        m_GameControl = asset.FindActionMap("GameControl", throwIfNotFound: true);
        m_GameControl_LeftClick = m_GameControl.FindAction("LeftClick", throwIfNotFound: true);
        m_GameControl_RightClick = m_GameControl.FindAction("RightClick", throwIfNotFound: true);
        m_GameControl_LeftHold = m_GameControl.FindAction("LeftHold", throwIfNotFound: true);
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

    // GameControl
    private readonly InputActionMap m_GameControl;
    private IGameControlActions m_GameControlActionsCallbackInterface;
    private readonly InputAction m_GameControl_LeftClick;
    private readonly InputAction m_GameControl_RightClick;
    private readonly InputAction m_GameControl_LeftHold;
    public struct GameControlActions
    {
        private @InputMaster m_Wrapper;
        public GameControlActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_GameControl_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_GameControl_RightClick;
        public InputAction @LeftHold => m_Wrapper.m_GameControl_LeftHold;
        public InputActionMap Get() { return m_Wrapper.m_GameControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameControlActions set) { return set.Get(); }
        public void SetCallbacks(IGameControlActions instance)
        {
            if (m_Wrapper.m_GameControlActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_GameControlActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_GameControlActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_GameControlActionsCallbackInterface.OnLeftClick;
                @RightClick.started -= m_Wrapper.m_GameControlActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_GameControlActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_GameControlActionsCallbackInterface.OnRightClick;
                @LeftHold.started -= m_Wrapper.m_GameControlActionsCallbackInterface.OnLeftHold;
                @LeftHold.performed -= m_Wrapper.m_GameControlActionsCallbackInterface.OnLeftHold;
                @LeftHold.canceled -= m_Wrapper.m_GameControlActionsCallbackInterface.OnLeftHold;
            }
            m_Wrapper.m_GameControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @LeftHold.started += instance.OnLeftHold;
                @LeftHold.performed += instance.OnLeftHold;
                @LeftHold.canceled += instance.OnLeftHold;
            }
        }
    }
    public GameControlActions @GameControl => new GameControlActions(this);
    public interface IGameControlActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnLeftHold(InputAction.CallbackContext context);
    }
}
