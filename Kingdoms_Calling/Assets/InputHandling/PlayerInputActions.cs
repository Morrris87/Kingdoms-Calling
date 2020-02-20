// GENERATED AUTOMATICALLY FROM 'Assets/InputHandling/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""52cc2c8c-5046-4290-9732-7e512b90ca56"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""4454ef00-6368-4a04-84a9-e65fa128035b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Show Items"",
                    ""type"": ""Button"",
                    ""id"": ""c53663b0-e4cc-4fad-a686-805d9a0a1bca"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""3d5334b9-22db-48c3-b9c5-0a7afb16b04f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""18fded2c-9159-4497-a705-cda6095ead3d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""b384b821-5291-463c-a3a8-fc16f68493e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CycleTargetF"",
                    ""type"": ""Button"",
                    ""id"": ""df5e734e-43d5-41db-a44d-ddd16440148a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CycleTargetB"",
                    ""type"": ""Button"",
                    ""id"": ""598227f3-7300-452a-a946-e8326b0d4f85"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""7668c7da-32f9-44c9-8617-d5cee4da4d69"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Value"",
                    ""id"": ""ba05a133-0b47-4c39-961c-a4c3e6d41fe2"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Value"",
                    ""id"": ""1c030335-1fe7-4234-a872-f415ddd4f5e5"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5c62cce7-b253-4d64-89a8-7a9fd382d223"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""df18e769-c2e0-40cb-9fc8-5df668e0b5ed"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HealthLoss"",
                    ""type"": ""Button"",
                    ""id"": ""aeb329f9-1ed8-463d-9428-7dec461cd4c5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HealthGain"",
                    ""type"": ""Button"",
                    ""id"": ""71b44987-ed4d-4eca-9f97-0d303228556d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""88b7ef19-0897-4c55-8bf3-17c077f7688e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.125,max=1)"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28915653-2d09-4812-8f02-7fba5cd7fb7b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Show Items"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b765103-1713-4426-972d-414677475bf9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45766794-659f-4004-a9b8-e96b179b8971"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f65b3e6f-e7ea-412b-8d65-3009f00beda1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""289f61a4-c05e-4f36-98ea-b2f574d4126d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleTargetF"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bba4085-8e48-405d-9f73-3e59bd6c802e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleTargetB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40c8d69f-78f1-474b-9010-6f756f3ac972"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa80b32a-eed3-47b9-8019-217d8d88408a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcc75c97-a26f-4617-b599-a169f389b660"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94c418e8-6b7f-4302-9ef6-bf2cde6c4947"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(pressPoint=1),Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f32377ef-91ae-43ac-90ed-8b79153225e9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60fc55a6-4dec-450f-b1a6-4c07dc701e57"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealthLoss"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d0eecbb-a821-4bf1-aec3-7e9bcc965bb6"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealthGain"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_ShowItems = m_PlayerControls.FindAction("Show Items", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
        m_PlayerControls_Interact = m_PlayerControls.FindAction("Interact", throwIfNotFound: true);
        m_PlayerControls_DropItem = m_PlayerControls.FindAction("DropItem", throwIfNotFound: true);
        m_PlayerControls_CycleTargetF = m_PlayerControls.FindAction("CycleTargetF", throwIfNotFound: true);
        m_PlayerControls_CycleTargetB = m_PlayerControls.FindAction("CycleTargetB", throwIfNotFound: true);
        m_PlayerControls_Rotate = m_PlayerControls.FindAction("Rotate", throwIfNotFound: true);
        m_PlayerControls_Ability1 = m_PlayerControls.FindAction("Ability1", throwIfNotFound: true);
        m_PlayerControls_Ability2 = m_PlayerControls.FindAction("Ability2", throwIfNotFound: true);
        m_PlayerControls_Special = m_PlayerControls.FindAction("Special", throwIfNotFound: true);
        m_PlayerControls_Attack = m_PlayerControls.FindAction("Attack", throwIfNotFound: true);
        m_PlayerControls_HealthLoss = m_PlayerControls.FindAction("HealthLoss", throwIfNotFound: true);
        m_PlayerControls_HealthGain = m_PlayerControls.FindAction("HealthGain", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_ShowItems;
    private readonly InputAction m_PlayerControls_Pause;
    private readonly InputAction m_PlayerControls_Interact;
    private readonly InputAction m_PlayerControls_DropItem;
    private readonly InputAction m_PlayerControls_CycleTargetF;
    private readonly InputAction m_PlayerControls_CycleTargetB;
    private readonly InputAction m_PlayerControls_Rotate;
    private readonly InputAction m_PlayerControls_Ability1;
    private readonly InputAction m_PlayerControls_Ability2;
    private readonly InputAction m_PlayerControls_Special;
    private readonly InputAction m_PlayerControls_Attack;
    private readonly InputAction m_PlayerControls_HealthLoss;
    private readonly InputAction m_PlayerControls_HealthGain;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @ShowItems => m_Wrapper.m_PlayerControls_ShowItems;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputAction @Interact => m_Wrapper.m_PlayerControls_Interact;
        public InputAction @DropItem => m_Wrapper.m_PlayerControls_DropItem;
        public InputAction @CycleTargetF => m_Wrapper.m_PlayerControls_CycleTargetF;
        public InputAction @CycleTargetB => m_Wrapper.m_PlayerControls_CycleTargetB;
        public InputAction @Rotate => m_Wrapper.m_PlayerControls_Rotate;
        public InputAction @Ability1 => m_Wrapper.m_PlayerControls_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_PlayerControls_Ability2;
        public InputAction @Special => m_Wrapper.m_PlayerControls_Special;
        public InputAction @Attack => m_Wrapper.m_PlayerControls_Attack;
        public InputAction @HealthLoss => m_Wrapper.m_PlayerControls_HealthLoss;
        public InputAction @HealthGain => m_Wrapper.m_PlayerControls_HealthGain;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @ShowItems.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShowItems;
                @ShowItems.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShowItems;
                @ShowItems.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShowItems;
                @Pause.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Interact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @DropItem.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDropItem;
                @CycleTargetF.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCycleTargetF;
                @CycleTargetF.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCycleTargetF;
                @CycleTargetF.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCycleTargetF;
                @CycleTargetB.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCycleTargetB;
                @CycleTargetB.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCycleTargetB;
                @CycleTargetB.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCycleTargetB;
                @Rotate.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotate;
                @Ability1.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAbility1;
                @Ability2.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAbility2;
                @Ability2.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAbility2;
                @Ability2.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAbility2;
                @Special.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecial;
                @Attack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttack;
                @HealthLoss.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnHealthLoss;
                @HealthLoss.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnHealthLoss;
                @HealthLoss.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnHealthLoss;
                @HealthGain.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnHealthGain;
                @HealthGain.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnHealthGain;
                @HealthGain.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnHealthGain;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ShowItems.started += instance.OnShowItems;
                @ShowItems.performed += instance.OnShowItems;
                @ShowItems.canceled += instance.OnShowItems;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @CycleTargetF.started += instance.OnCycleTargetF;
                @CycleTargetF.performed += instance.OnCycleTargetF;
                @CycleTargetF.canceled += instance.OnCycleTargetF;
                @CycleTargetB.started += instance.OnCycleTargetB;
                @CycleTargetB.performed += instance.OnCycleTargetB;
                @CycleTargetB.canceled += instance.OnCycleTargetB;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Ability2.started += instance.OnAbility2;
                @Ability2.performed += instance.OnAbility2;
                @Ability2.canceled += instance.OnAbility2;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @HealthLoss.started += instance.OnHealthLoss;
                @HealthLoss.performed += instance.OnHealthLoss;
                @HealthLoss.canceled += instance.OnHealthLoss;
                @HealthGain.started += instance.OnHealthGain;
                @HealthGain.performed += instance.OnHealthGain;
                @HealthGain.canceled += instance.OnHealthGain;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShowItems(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
        void OnCycleTargetF(InputAction.CallbackContext context);
        void OnCycleTargetB(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnHealthLoss(InputAction.CallbackContext context);
        void OnHealthGain(InputAction.CallbackContext context);
    }
}
