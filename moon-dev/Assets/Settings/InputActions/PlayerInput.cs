//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Settings/InputActions/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMotion"",
            ""id"": ""b08f03bb-6ffc-40f6-83a9-0213de34ebf2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f5c6222b-74cc-410c-8736-0cb4b37f9c43"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e73ed3f3-7a67-40f4-94d6-af884149236b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""9c98bb30-73e6-45c7-8e32-2a18992f4d88"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slice"",
                    ""type"": ""Button"",
                    ""id"": ""cdf6a16e-de3c-4419-b1c3-1b52a8f15385"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b0eab64e-3c52-436c-87bd-12434ee7ca65"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f8dc187d-8a09-47da-b957-6066058168a0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1c108b28-d43b-4845-9d57-89561481c200"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""71fee988-be16-4134-9e84-97b5d8e9751b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""278bd79b-b44e-4d12-be1f-f51c4088e2d9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""33355f68-a013-4009-871d-609134bc8c63"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f57c2c18-8f19-4273-992b-ad3d9bf487ea"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20953cc3-e836-4e8f-a7d1-be657c9752b8"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debugger"",
            ""id"": ""4cf33d12-3205-4d8f-babc-c5cae1dc0812"",
            ""actions"": [
                {
                    ""name"": ""Num1"",
                    ""type"": ""Value"",
                    ""id"": ""5d717891-86b6-488d-bc63-560d9fdbefa6"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Num2"",
                    ""type"": ""Button"",
                    ""id"": ""45f24c21-de1f-4fe7-b0c1-c4964980cfc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num3"",
                    ""type"": ""Button"",
                    ""id"": ""b2adbc1c-23e2-4af3-8066-1095b87298f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num4"",
                    ""type"": ""Button"",
                    ""id"": ""15ad083e-7539-437e-a8ab-06f53e7628bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Num5"",
                    ""type"": ""Button"",
                    ""id"": ""673da418-c86f-4073-be0c-13a3e1d04d75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""92675584-e303-44a7-b0b8-b787a7bcb926"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""393f2e32-309c-4d98-84ac-33cef61c7c81"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb42c541-2b10-4f96-99fd-8e1e662fc51f"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""875b1717-4e88-4e76-9d82-f34562dd053a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""915b6cf6-44ba-4da8-88ce-eaa81c7ad1cf"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Num5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMotion
        m_PlayerMotion = asset.FindActionMap("PlayerMotion", throwIfNotFound: true);
        m_PlayerMotion_Move = m_PlayerMotion.FindAction("Move", throwIfNotFound: true);
        m_PlayerMotion_Jump = m_PlayerMotion.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMotion_Run = m_PlayerMotion.FindAction("Run", throwIfNotFound: true);
        m_PlayerMotion_Slice = m_PlayerMotion.FindAction("Slice", throwIfNotFound: true);
        // Debugger
        m_Debugger = asset.FindActionMap("Debugger", throwIfNotFound: true);
        m_Debugger_Num1 = m_Debugger.FindAction("Num1", throwIfNotFound: true);
        m_Debugger_Num2 = m_Debugger.FindAction("Num2", throwIfNotFound: true);
        m_Debugger_Num3 = m_Debugger.FindAction("Num3", throwIfNotFound: true);
        m_Debugger_Num4 = m_Debugger.FindAction("Num4", throwIfNotFound: true);
        m_Debugger_Num5 = m_Debugger.FindAction("Num5", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMotion
    private readonly InputActionMap m_PlayerMotion;
    private List<IPlayerMotionActions> m_PlayerMotionActionsCallbackInterfaces = new List<IPlayerMotionActions>();
    private readonly InputAction m_PlayerMotion_Move;
    private readonly InputAction m_PlayerMotion_Jump;
    private readonly InputAction m_PlayerMotion_Run;
    private readonly InputAction m_PlayerMotion_Slice;
    public struct PlayerMotionActions
    {
        private @PlayerAction m_Wrapper;
        public PlayerMotionActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMotion_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerMotion_Jump;
        public InputAction @Run => m_Wrapper.m_PlayerMotion_Run;
        public InputAction @Slice => m_Wrapper.m_PlayerMotion_Slice;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMotion; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMotionActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMotionActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMotionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMotionActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Slice.started += instance.OnSlice;
            @Slice.performed += instance.OnSlice;
            @Slice.canceled += instance.OnSlice;
        }

        private void UnregisterCallbacks(IPlayerMotionActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Slice.started -= instance.OnSlice;
            @Slice.performed -= instance.OnSlice;
            @Slice.canceled -= instance.OnSlice;
        }

        public void RemoveCallbacks(IPlayerMotionActions instance)
        {
            if (m_Wrapper.m_PlayerMotionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMotionActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMotionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMotionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMotionActions @PlayerMotion => new PlayerMotionActions(this);

    // Debugger
    private readonly InputActionMap m_Debugger;
    private List<IDebuggerActions> m_DebuggerActionsCallbackInterfaces = new List<IDebuggerActions>();
    private readonly InputAction m_Debugger_Num1;
    private readonly InputAction m_Debugger_Num2;
    private readonly InputAction m_Debugger_Num3;
    private readonly InputAction m_Debugger_Num4;
    private readonly InputAction m_Debugger_Num5;
    public struct DebuggerActions
    {
        private @PlayerAction m_Wrapper;
        public DebuggerActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Num1 => m_Wrapper.m_Debugger_Num1;
        public InputAction @Num2 => m_Wrapper.m_Debugger_Num2;
        public InputAction @Num3 => m_Wrapper.m_Debugger_Num3;
        public InputAction @Num4 => m_Wrapper.m_Debugger_Num4;
        public InputAction @Num5 => m_Wrapper.m_Debugger_Num5;
        public InputActionMap Get() { return m_Wrapper.m_Debugger; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebuggerActions set) { return set.Get(); }
        public void AddCallbacks(IDebuggerActions instance)
        {
            if (instance == null || m_Wrapper.m_DebuggerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DebuggerActionsCallbackInterfaces.Add(instance);
            @Num1.started += instance.OnNum1;
            @Num1.performed += instance.OnNum1;
            @Num1.canceled += instance.OnNum1;
            @Num2.started += instance.OnNum2;
            @Num2.performed += instance.OnNum2;
            @Num2.canceled += instance.OnNum2;
            @Num3.started += instance.OnNum3;
            @Num3.performed += instance.OnNum3;
            @Num3.canceled += instance.OnNum3;
            @Num4.started += instance.OnNum4;
            @Num4.performed += instance.OnNum4;
            @Num4.canceled += instance.OnNum4;
            @Num5.started += instance.OnNum5;
            @Num5.performed += instance.OnNum5;
            @Num5.canceled += instance.OnNum5;
        }

        private void UnregisterCallbacks(IDebuggerActions instance)
        {
            @Num1.started -= instance.OnNum1;
            @Num1.performed -= instance.OnNum1;
            @Num1.canceled -= instance.OnNum1;
            @Num2.started -= instance.OnNum2;
            @Num2.performed -= instance.OnNum2;
            @Num2.canceled -= instance.OnNum2;
            @Num3.started -= instance.OnNum3;
            @Num3.performed -= instance.OnNum3;
            @Num3.canceled -= instance.OnNum3;
            @Num4.started -= instance.OnNum4;
            @Num4.performed -= instance.OnNum4;
            @Num4.canceled -= instance.OnNum4;
            @Num5.started -= instance.OnNum5;
            @Num5.performed -= instance.OnNum5;
            @Num5.canceled -= instance.OnNum5;
        }

        public void RemoveCallbacks(IDebuggerActions instance)
        {
            if (m_Wrapper.m_DebuggerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDebuggerActions instance)
        {
            foreach (var item in m_Wrapper.m_DebuggerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DebuggerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DebuggerActions @Debugger => new DebuggerActions(this);
    public interface IPlayerMotionActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnSlice(InputAction.CallbackContext context);
    }
    public interface IDebuggerActions
    {
        void OnNum1(InputAction.CallbackContext context);
        void OnNum2(InputAction.CallbackContext context);
        void OnNum3(InputAction.CallbackContext context);
        void OnNum4(InputAction.CallbackContext context);
        void OnNum5(InputAction.CallbackContext context);
    }
}