//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""5dd981cd-3d00-4bc9-adb3-40049b6a0187"",
            ""actions"": [
                {
                    ""name"": ""MoveStick"",
                    ""type"": ""Value"",
                    ""id"": ""2bd62f8b-a167-48bc-b473-8067eee55114"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveW"",
                    ""type"": ""Button"",
                    ""id"": ""9b1ec5f2-7b49-4850-be64-539d686c45b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveS"",
                    ""type"": ""Button"",
                    ""id"": ""0410a045-0cf5-48d1-baff-581001b1a3a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveA"",
                    ""type"": ""Button"",
                    ""id"": ""0a866deb-4aa9-4415-901f-1ffd6f01c102"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveD"",
                    ""type"": ""Button"",
                    ""id"": ""887ff212-e10f-40ff-842e-2528c186cbe4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AimStick"",
                    ""type"": ""Value"",
                    ""id"": ""1a89f0c6-5fff-4d7b-8133-724347dddb2b"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShootR"",
                    ""type"": ""Button"",
                    ""id"": ""acf3df24-5323-4ac3-978c-83733fefd7fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootL"",
                    ""type"": ""Button"",
                    ""id"": ""949acae8-d62f-41b8-894c-0f975f96be4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""92c0a039-cd29-4cd8-bae7-c56360767e8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""APress"",
                    ""type"": ""Button"",
                    ""id"": ""0f72b60f-a29c-4f1c-b26d-82acff4acf1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootRB"",
                    ""type"": ""Button"",
                    ""id"": ""98e08e22-72fd-4171-9526-063f22ac2d78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootLB"",
                    ""type"": ""Button"",
                    ""id"": ""304752c8-ca67-4480-a2fe-5f64e82a4a51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90609038-5cb2-413e-93c1-4fd7862b43fc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32895561-7415-4f43-9318-b04fa3c09130"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveW"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32fb4037-7929-4246-9ff9-e6cc5e035cd8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d04227cc-c8e6-45fd-b67f-79412eab19ae"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fc4d43c-2fb5-4dc5-a3a9-5b19c1e86578"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49c51071-e528-4483-a00b-5548ccb31b01"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""932f366f-4476-4962-88d3-d783685c2f51"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""170722bf-094c-4a04-bfe6-1a9924873cb1"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""089b6816-8f07-4148-9a0c-dbf25a852117"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4542d682-d3e9-4139-bcf2-3530099d9107"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""APress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""725feb31-6bb1-430d-b69d-dabc1c4df448"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootRB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b86635a-8b05-4b10-8104-a89401b2b12b"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootLB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveStick = m_Gameplay.FindAction("MoveStick", throwIfNotFound: true);
        m_Gameplay_MoveW = m_Gameplay.FindAction("MoveW", throwIfNotFound: true);
        m_Gameplay_MoveS = m_Gameplay.FindAction("MoveS", throwIfNotFound: true);
        m_Gameplay_MoveA = m_Gameplay.FindAction("MoveA", throwIfNotFound: true);
        m_Gameplay_MoveD = m_Gameplay.FindAction("MoveD", throwIfNotFound: true);
        m_Gameplay_AimStick = m_Gameplay.FindAction("AimStick", throwIfNotFound: true);
        m_Gameplay_ShootR = m_Gameplay.FindAction("ShootR", throwIfNotFound: true);
        m_Gameplay_ShootL = m_Gameplay.FindAction("ShootL", throwIfNotFound: true);
        m_Gameplay_Start = m_Gameplay.FindAction("Start", throwIfNotFound: true);
        m_Gameplay_APress = m_Gameplay.FindAction("APress", throwIfNotFound: true);
        m_Gameplay_ShootRB = m_Gameplay.FindAction("ShootRB", throwIfNotFound: true);
        m_Gameplay_ShootLB = m_Gameplay.FindAction("ShootLB", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MoveStick;
    private readonly InputAction m_Gameplay_MoveW;
    private readonly InputAction m_Gameplay_MoveS;
    private readonly InputAction m_Gameplay_MoveA;
    private readonly InputAction m_Gameplay_MoveD;
    private readonly InputAction m_Gameplay_AimStick;
    private readonly InputAction m_Gameplay_ShootR;
    private readonly InputAction m_Gameplay_ShootL;
    private readonly InputAction m_Gameplay_Start;
    private readonly InputAction m_Gameplay_APress;
    private readonly InputAction m_Gameplay_ShootRB;
    private readonly InputAction m_Gameplay_ShootLB;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveStick => m_Wrapper.m_Gameplay_MoveStick;
        public InputAction @MoveW => m_Wrapper.m_Gameplay_MoveW;
        public InputAction @MoveS => m_Wrapper.m_Gameplay_MoveS;
        public InputAction @MoveA => m_Wrapper.m_Gameplay_MoveA;
        public InputAction @MoveD => m_Wrapper.m_Gameplay_MoveD;
        public InputAction @AimStick => m_Wrapper.m_Gameplay_AimStick;
        public InputAction @ShootR => m_Wrapper.m_Gameplay_ShootR;
        public InputAction @ShootL => m_Wrapper.m_Gameplay_ShootL;
        public InputAction @Start => m_Wrapper.m_Gameplay_Start;
        public InputAction @APress => m_Wrapper.m_Gameplay_APress;
        public InputAction @ShootRB => m_Wrapper.m_Gameplay_ShootRB;
        public InputAction @ShootLB => m_Wrapper.m_Gameplay_ShootLB;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MoveStick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveStick;
                @MoveStick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveStick;
                @MoveStick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveStick;
                @MoveW.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveW;
                @MoveW.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveW;
                @MoveW.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveW;
                @MoveS.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveS;
                @MoveS.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveS;
                @MoveS.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveS;
                @MoveA.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveA;
                @MoveA.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveA;
                @MoveA.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveA;
                @MoveD.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveD;
                @MoveD.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveD;
                @MoveD.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveD;
                @AimStick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimStick;
                @AimStick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimStick;
                @AimStick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAimStick;
                @ShootR.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootR;
                @ShootR.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootR;
                @ShootR.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootR;
                @ShootL.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootL;
                @ShootL.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootL;
                @ShootL.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootL;
                @Start.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @APress.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAPress;
                @APress.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAPress;
                @APress.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAPress;
                @ShootRB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootRB;
                @ShootRB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootRB;
                @ShootRB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootRB;
                @ShootLB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootLB;
                @ShootLB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootLB;
                @ShootLB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootLB;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveStick.started += instance.OnMoveStick;
                @MoveStick.performed += instance.OnMoveStick;
                @MoveStick.canceled += instance.OnMoveStick;
                @MoveW.started += instance.OnMoveW;
                @MoveW.performed += instance.OnMoveW;
                @MoveW.canceled += instance.OnMoveW;
                @MoveS.started += instance.OnMoveS;
                @MoveS.performed += instance.OnMoveS;
                @MoveS.canceled += instance.OnMoveS;
                @MoveA.started += instance.OnMoveA;
                @MoveA.performed += instance.OnMoveA;
                @MoveA.canceled += instance.OnMoveA;
                @MoveD.started += instance.OnMoveD;
                @MoveD.performed += instance.OnMoveD;
                @MoveD.canceled += instance.OnMoveD;
                @AimStick.started += instance.OnAimStick;
                @AimStick.performed += instance.OnAimStick;
                @AimStick.canceled += instance.OnAimStick;
                @ShootR.started += instance.OnShootR;
                @ShootR.performed += instance.OnShootR;
                @ShootR.canceled += instance.OnShootR;
                @ShootL.started += instance.OnShootL;
                @ShootL.performed += instance.OnShootL;
                @ShootL.canceled += instance.OnShootL;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @APress.started += instance.OnAPress;
                @APress.performed += instance.OnAPress;
                @APress.canceled += instance.OnAPress;
                @ShootRB.started += instance.OnShootRB;
                @ShootRB.performed += instance.OnShootRB;
                @ShootRB.canceled += instance.OnShootRB;
                @ShootLB.started += instance.OnShootLB;
                @ShootLB.performed += instance.OnShootLB;
                @ShootLB.canceled += instance.OnShootLB;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveStick(InputAction.CallbackContext context);
        void OnMoveW(InputAction.CallbackContext context);
        void OnMoveS(InputAction.CallbackContext context);
        void OnMoveA(InputAction.CallbackContext context);
        void OnMoveD(InputAction.CallbackContext context);
        void OnAimStick(InputAction.CallbackContext context);
        void OnShootR(InputAction.CallbackContext context);
        void OnShootL(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnAPress(InputAction.CallbackContext context);
        void OnShootRB(InputAction.CallbackContext context);
        void OnShootLB(InputAction.CallbackContext context);
    }
}
