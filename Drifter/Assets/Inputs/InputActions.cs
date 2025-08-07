// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""MainGame"",
            ""id"": ""75e4a382-d80c-4163-984b-fc68fb007c16"",
            ""actions"": [
                {
                    ""name"": ""Steer"",
                    ""type"": ""Value"",
                    ""id"": ""e10432ac-2d87-401b-b950-5fc8cc20105e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""904c63db-9db4-42b6-b53b-6f595012b954"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Value"",
                    ""id"": ""c67bb412-9bbf-4fa0-98a9-ad0aeff5bf98"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d72a33bc-f5d7-4243-a6b2-4f32068d981c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=-1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffed2797-ce3e-45dd-87a6-d8d82cfaa890"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6258dea-23fe-4c65-a6c9-4f4348d8f4cd"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""697ccdad-3119-4365-ad45-9ffebe061cab"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=-1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf643d83-7f95-4b3e-98ec-47e8621bb9bd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""406fd53c-6924-463d-bde2-18b72cb7cc3d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb5473e5-55d5-42fa-b2e1-519bcdb9bb37"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05f29083-741b-4dde-8fe0-240d8b0d9086"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MainGame
        m_MainGame = asset.FindActionMap("MainGame", throwIfNotFound: true);
        m_MainGame_Steer = m_MainGame.FindAction("Steer", throwIfNotFound: true);
        m_MainGame_Accelerate = m_MainGame.FindAction("Accelerate", throwIfNotFound: true);
        m_MainGame_Brake = m_MainGame.FindAction("Brake", throwIfNotFound: true);
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

    // MainGame
    private readonly InputActionMap m_MainGame;
    private IMainGameActions m_MainGameActionsCallbackInterface;
    private readonly InputAction m_MainGame_Steer;
    private readonly InputAction m_MainGame_Accelerate;
    private readonly InputAction m_MainGame_Brake;
    public struct MainGameActions
    {
        private @InputActions m_Wrapper;
        public MainGameActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Steer => m_Wrapper.m_MainGame_Steer;
        public InputAction @Accelerate => m_Wrapper.m_MainGame_Accelerate;
        public InputAction @Brake => m_Wrapper.m_MainGame_Brake;
        public InputActionMap Get() { return m_Wrapper.m_MainGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainGameActions set) { return set.Get(); }
        public void SetCallbacks(IMainGameActions instance)
        {
            if (m_Wrapper.m_MainGameActionsCallbackInterface != null)
            {
                @Steer.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnSteer;
                @Steer.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnSteer;
                @Steer.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnSteer;
                @Accelerate.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnAccelerate;
                @Brake.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnBrake;
                @Brake.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnBrake;
                @Brake.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnBrake;
            }
            m_Wrapper.m_MainGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Steer.started += instance.OnSteer;
                @Steer.performed += instance.OnSteer;
                @Steer.canceled += instance.OnSteer;
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
            }
        }
    }
    public MainGameActions @MainGame => new MainGameActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMainGameActions
    {
        void OnSteer(InputAction.CallbackContext context);
        void OnAccelerate(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
    }
}
