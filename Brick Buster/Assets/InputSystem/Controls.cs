//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/InputSystem/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""AppControls"",
            ""id"": ""8a8784d0-752b-41ed-9675-688160e588ac"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""db4e498f-cab3-4c35-88c3-0e526f8f4d85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1a6d7cbf-44cb-49e8-b4e9-394f446aba57"",
                    ""path"": ""*/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // AppControls
        m_AppControls = asset.FindActionMap("AppControls", throwIfNotFound: true);
        m_AppControls_Back = m_AppControls.FindAction("Back", throwIfNotFound: true);
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

    // AppControls
    private readonly InputActionMap m_AppControls;
    private List<IAppControlsActions> m_AppControlsActionsCallbackInterfaces = new List<IAppControlsActions>();
    private readonly InputAction m_AppControls_Back;
    public struct AppControlsActions
    {
        private @Controls m_Wrapper;
        public AppControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Back => m_Wrapper.m_AppControls_Back;
        public InputActionMap Get() { return m_Wrapper.m_AppControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AppControlsActions set) { return set.Get(); }
        public void AddCallbacks(IAppControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_AppControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_AppControlsActionsCallbackInterfaces.Add(instance);
            @Back.started += instance.OnBack;
            @Back.performed += instance.OnBack;
            @Back.canceled += instance.OnBack;
        }

        private void UnregisterCallbacks(IAppControlsActions instance)
        {
            @Back.started -= instance.OnBack;
            @Back.performed -= instance.OnBack;
            @Back.canceled -= instance.OnBack;
        }

        public void RemoveCallbacks(IAppControlsActions instance)
        {
            if (m_Wrapper.m_AppControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IAppControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_AppControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_AppControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public AppControlsActions @AppControls => new AppControlsActions(this);
    public interface IAppControlsActions
    {
        void OnBack(InputAction.CallbackContext context);
    }
}
