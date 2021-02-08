// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f7b18825-9894-4ffe-ac06-ffd1c5d6e869"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b842817c-fd3d-4b1f-a77e-f1836e30d2b4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveInput"",
                    ""type"": ""Value"",
                    ""id"": ""803d21d4-e7ce-439a-8dd1-afb86343cd9b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpInput"",
                    ""type"": ""Value"",
                    ""id"": ""ecef1674-c3b8-4a2f-b2f4-38096f9039d1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""SprintInput"",
                    ""type"": ""Button"",
                    ""id"": ""d3b4b11f-1fca-4017-929f-a86aed0a0f51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Value"",
                    ""id"": ""247ffa92-4e9f-49c9-946f-d85325e2b662"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Settings"",
                    ""type"": ""Button"",
                    ""id"": ""eddf44a3-c257-4a3e-9149-5612ebe12692"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scoreboard"",
                    ""type"": ""Value"",
                    ""id"": ""e210af2b-dd72-4a5a-a7b4-76421ed5fc91"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""7e5bf3b6-b76a-4eb0-968e-ab492ceb4fcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CrouchInput"",
                    ""type"": ""Value"",
                    ""id"": ""f820c198-50c1-4dfa-b7f7-b80c45d4c37a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimInput"",
                    ""type"": ""Value"",
                    ""id"": ""2c72443d-938a-4f33-a0f6-8c6783dba6fc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Setiings"",
                    ""type"": ""Button"",
                    ""id"": ""040527d5-c82e-401c-bbc3-a7c024032afa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ab2c9e5e-ec00-4a09-85ff-1a5c72e1cf53"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""681d44c6-d64d-451d-9cb8-374c7046a012"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3db97bfc-12f2-460e-a663-31c52baa20dd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""72c9953d-0b51-42eb-a079-0fd837be6dd7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4c14a335-dfb8-4583-b2ec-e25343a61d24"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""20620af2-639a-49b0-ba8f-c6fab1ddd848"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""JumpInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1344e803-f521-4305-be4d-804bcb187620"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0b72871-df3f-477a-a080-b85eac16a0d6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""SprintInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a285941b-de01-400c-8401-51d86565ff47"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5021b1b3-f407-4341-a944-1b402d43789e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc72a9cf-4896-486a-8995-f52aeb477b81"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Scoreboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed1383b7-4462-4b05-a98e-540bb1ce9bde"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb2b16d3-26b2-4de4-baa9-82e407605bab"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""CrouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ab5aad1-54db-4805-b296-b4a360df0bd7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""AimInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ac907b2-cd70-4574-8f86-596226a121f3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Setiings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DeadPlayer"",
            ""id"": ""71138131-ce3a-4470-95ff-7b40351daf0f"",
            ""actions"": [
                {
                    ""name"": ""ChangeCam"",
                    ""type"": ""Button"",
                    ""id"": ""5a3bd6c4-a647-483a-8943-24daef2af99b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""441c918f-3b3f-44f9-821a-7ab54bb4fb37"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""ChangeCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & mouse"",
            ""bindingGroup"": ""Keyboard & mouse"",
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
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_MoveInput = m_Player.FindAction("MoveInput", throwIfNotFound: true);
        m_Player_JumpInput = m_Player.FindAction("JumpInput", throwIfNotFound: true);
        m_Player_SprintInput = m_Player.FindAction("SprintInput", throwIfNotFound: true);
        m_Player_Fire1 = m_Player.FindAction("Fire1", throwIfNotFound: true);
        m_Player_Settings = m_Player.FindAction("Settings", throwIfNotFound: true);
        m_Player_Scoreboard = m_Player.FindAction("Scoreboard", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_CrouchInput = m_Player.FindAction("CrouchInput", throwIfNotFound: true);
        m_Player_AimInput = m_Player.FindAction("AimInput", throwIfNotFound: true);
        m_Player_Setiings = m_Player.FindAction("Setiings", throwIfNotFound: true);
        // DeadPlayer
        m_DeadPlayer = asset.FindActionMap("DeadPlayer", throwIfNotFound: true);
        m_DeadPlayer_ChangeCam = m_DeadPlayer.FindAction("ChangeCam", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_MoveInput;
    private readonly InputAction m_Player_JumpInput;
    private readonly InputAction m_Player_SprintInput;
    private readonly InputAction m_Player_Fire1;
    private readonly InputAction m_Player_Settings;
    private readonly InputAction m_Player_Scoreboard;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_CrouchInput;
    private readonly InputAction m_Player_AimInput;
    private readonly InputAction m_Player_Setiings;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @MoveInput => m_Wrapper.m_Player_MoveInput;
        public InputAction @JumpInput => m_Wrapper.m_Player_JumpInput;
        public InputAction @SprintInput => m_Wrapper.m_Player_SprintInput;
        public InputAction @Fire1 => m_Wrapper.m_Player_Fire1;
        public InputAction @Settings => m_Wrapper.m_Player_Settings;
        public InputAction @Scoreboard => m_Wrapper.m_Player_Scoreboard;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @CrouchInput => m_Wrapper.m_Player_CrouchInput;
        public InputAction @AimInput => m_Wrapper.m_Player_AimInput;
        public InputAction @Setiings => m_Wrapper.m_Player_Setiings;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @MoveInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveInput;
                @MoveInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveInput;
                @MoveInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveInput;
                @JumpInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpInput;
                @JumpInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpInput;
                @JumpInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpInput;
                @SprintInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprintInput;
                @SprintInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprintInput;
                @SprintInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprintInput;
                @Fire1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Settings.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Settings.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Settings.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Scoreboard.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScoreboard;
                @Scoreboard.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScoreboard;
                @Scoreboard.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScoreboard;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @CrouchInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouchInput;
                @CrouchInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouchInput;
                @CrouchInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouchInput;
                @AimInput.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAimInput;
                @AimInput.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAimInput;
                @AimInput.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAimInput;
                @Setiings.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetiings;
                @Setiings.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetiings;
                @Setiings.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetiings;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @MoveInput.started += instance.OnMoveInput;
                @MoveInput.performed += instance.OnMoveInput;
                @MoveInput.canceled += instance.OnMoveInput;
                @JumpInput.started += instance.OnJumpInput;
                @JumpInput.performed += instance.OnJumpInput;
                @JumpInput.canceled += instance.OnJumpInput;
                @SprintInput.started += instance.OnSprintInput;
                @SprintInput.performed += instance.OnSprintInput;
                @SprintInput.canceled += instance.OnSprintInput;
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
                @Settings.started += instance.OnSettings;
                @Settings.performed += instance.OnSettings;
                @Settings.canceled += instance.OnSettings;
                @Scoreboard.started += instance.OnScoreboard;
                @Scoreboard.performed += instance.OnScoreboard;
                @Scoreboard.canceled += instance.OnScoreboard;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @CrouchInput.started += instance.OnCrouchInput;
                @CrouchInput.performed += instance.OnCrouchInput;
                @CrouchInput.canceled += instance.OnCrouchInput;
                @AimInput.started += instance.OnAimInput;
                @AimInput.performed += instance.OnAimInput;
                @AimInput.canceled += instance.OnAimInput;
                @Setiings.started += instance.OnSetiings;
                @Setiings.performed += instance.OnSetiings;
                @Setiings.canceled += instance.OnSetiings;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // DeadPlayer
    private readonly InputActionMap m_DeadPlayer;
    private IDeadPlayerActions m_DeadPlayerActionsCallbackInterface;
    private readonly InputAction m_DeadPlayer_ChangeCam;
    public struct DeadPlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public DeadPlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeCam => m_Wrapper.m_DeadPlayer_ChangeCam;
        public InputActionMap Get() { return m_Wrapper.m_DeadPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DeadPlayerActions set) { return set.Get(); }
        public void SetCallbacks(IDeadPlayerActions instance)
        {
            if (m_Wrapper.m_DeadPlayerActionsCallbackInterface != null)
            {
                @ChangeCam.started -= m_Wrapper.m_DeadPlayerActionsCallbackInterface.OnChangeCam;
                @ChangeCam.performed -= m_Wrapper.m_DeadPlayerActionsCallbackInterface.OnChangeCam;
                @ChangeCam.canceled -= m_Wrapper.m_DeadPlayerActionsCallbackInterface.OnChangeCam;
            }
            m_Wrapper.m_DeadPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeCam.started += instance.OnChangeCam;
                @ChangeCam.performed += instance.OnChangeCam;
                @ChangeCam.canceled += instance.OnChangeCam;
            }
        }
    }
    public DeadPlayerActions @DeadPlayer => new DeadPlayerActions(this);
    private int m_KeyboardmouseSchemeIndex = -1;
    public InputControlScheme KeyboardmouseScheme
    {
        get
        {
            if (m_KeyboardmouseSchemeIndex == -1) m_KeyboardmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & mouse");
            return asset.controlSchemes[m_KeyboardmouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMoveInput(InputAction.CallbackContext context);
        void OnJumpInput(InputAction.CallbackContext context);
        void OnSprintInput(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
        void OnSettings(InputAction.CallbackContext context);
        void OnScoreboard(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnCrouchInput(InputAction.CallbackContext context);
        void OnAimInput(InputAction.CallbackContext context);
        void OnSetiings(InputAction.CallbackContext context);
    }
    public interface IDeadPlayerActions
    {
        void OnChangeCam(InputAction.CallbackContext context);
    }
}
