//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/PlayerControls.inputactions
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
            ""id"": ""e0d2ad24-05e3-4967-8cce-a5c9024a171e"",
            ""actions"": [
                {
                    ""name"": ""PlayerMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c6d4c64d-138c-4f3d-94ee-e647c524eb53"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""35452f53-730f-424c-8454-c7df377d8405"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerJump"",
                    ""type"": ""Button"",
                    ""id"": ""22d9abc6-2e88-4c84-84c4-b63ce9d2e53e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerAttack"",
                    ""type"": ""Button"",
                    ""id"": ""5167c2b6-3ee8-4ee4-aeee-ef96bbc1fbd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PickUpItem"",
                    ""type"": ""Button"",
                    ""id"": ""9ea2d026-256d-4824-ad9b-35cfc786f1e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerRoll"",
                    ""type"": ""Button"",
                    ""id"": ""e973a698-447a-494d-a2ca-14abad90a0a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerRevive"",
                    ""type"": ""Button"",
                    ""id"": ""f8e4d807-7fb7-49e5-a499-a877d5d78ec6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9da83b13-d061-445e-828e-ea452ca3bab2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf8465c8-9503-4b80-af5e-2bfab1b45d0a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fb8aa05-48cb-4ad2-bf9b-08b9fd383678"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerJump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bed5d1c-4dc2-4268-899e-04241299d456"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6d73946-e822-4903-8e33-f404cb30f9bc"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUpItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d438f3d0-a21d-49ac-8e47-cbd7ea013e68"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd6ae000-a9ea-4cc0-9356-9d24f19522b4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerRevive"",
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
        m_Gameplay_PlayerMove = m_Gameplay.FindAction("PlayerMove", throwIfNotFound: true);
        m_Gameplay_CameraMove = m_Gameplay.FindAction("CameraMove", throwIfNotFound: true);
        m_Gameplay_PlayerJump = m_Gameplay.FindAction("PlayerJump", throwIfNotFound: true);
        m_Gameplay_PlayerAttack = m_Gameplay.FindAction("PlayerAttack", throwIfNotFound: true);
        m_Gameplay_PickUpItem = m_Gameplay.FindAction("PickUpItem", throwIfNotFound: true);
        m_Gameplay_PlayerRoll = m_Gameplay.FindAction("PlayerRoll", throwIfNotFound: true);
        m_Gameplay_PlayerRevive = m_Gameplay.FindAction("PlayerRevive", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_PlayerMove;
    private readonly InputAction m_Gameplay_CameraMove;
    private readonly InputAction m_Gameplay_PlayerJump;
    private readonly InputAction m_Gameplay_PlayerAttack;
    private readonly InputAction m_Gameplay_PickUpItem;
    private readonly InputAction m_Gameplay_PlayerRoll;
    private readonly InputAction m_Gameplay_PlayerRevive;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMove => m_Wrapper.m_Gameplay_PlayerMove;
        public InputAction @CameraMove => m_Wrapper.m_Gameplay_CameraMove;
        public InputAction @PlayerJump => m_Wrapper.m_Gameplay_PlayerJump;
        public InputAction @PlayerAttack => m_Wrapper.m_Gameplay_PlayerAttack;
        public InputAction @PickUpItem => m_Wrapper.m_Gameplay_PickUpItem;
        public InputAction @PlayerRoll => m_Wrapper.m_Gameplay_PlayerRoll;
        public InputAction @PlayerRevive => m_Wrapper.m_Gameplay_PlayerRevive;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @PlayerMove.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMove;
                @CameraMove.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraMove;
                @PlayerJump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerJump;
                @PlayerJump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerJump;
                @PlayerJump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerJump;
                @PlayerAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerAttack;
                @PlayerAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerAttack;
                @PlayerAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerAttack;
                @PickUpItem.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickUpItem;
                @PickUpItem.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickUpItem;
                @PickUpItem.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickUpItem;
                @PlayerRoll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRoll;
                @PlayerRoll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRoll;
                @PlayerRoll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRoll;
                @PlayerRevive.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRevive;
                @PlayerRevive.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRevive;
                @PlayerRevive.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerRevive;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMove.started += instance.OnPlayerMove;
                @PlayerMove.performed += instance.OnPlayerMove;
                @PlayerMove.canceled += instance.OnPlayerMove;
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @PlayerJump.started += instance.OnPlayerJump;
                @PlayerJump.performed += instance.OnPlayerJump;
                @PlayerJump.canceled += instance.OnPlayerJump;
                @PlayerAttack.started += instance.OnPlayerAttack;
                @PlayerAttack.performed += instance.OnPlayerAttack;
                @PlayerAttack.canceled += instance.OnPlayerAttack;
                @PickUpItem.started += instance.OnPickUpItem;
                @PickUpItem.performed += instance.OnPickUpItem;
                @PickUpItem.canceled += instance.OnPickUpItem;
                @PlayerRoll.started += instance.OnPlayerRoll;
                @PlayerRoll.performed += instance.OnPlayerRoll;
                @PlayerRoll.canceled += instance.OnPlayerRoll;
                @PlayerRevive.started += instance.OnPlayerRevive;
                @PlayerRevive.performed += instance.OnPlayerRevive;
                @PlayerRevive.canceled += instance.OnPlayerRevive;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnPlayerMove(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
        void OnPlayerJump(InputAction.CallbackContext context);
        void OnPlayerAttack(InputAction.CallbackContext context);
        void OnPickUpItem(InputAction.CallbackContext context);
        void OnPlayerRoll(InputAction.CallbackContext context);
        void OnPlayerRevive(InputAction.CallbackContext context);
    }
}
