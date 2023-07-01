using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenuLaunchHandler : MonoBehaviour
{
    private InputActions _inputActions;

    public void Enter()
    {
        _inputActions.Ingame.Enable();
        _inputActions.Ingame.Backpackmenu.performed += Backpackmenu;
        _inputActions.Ingame.Settingsmenu.performed += Settingsmenu;
    }

    private void Awake()
    {
        _inputActions = new InputActions();
        Enter();
    }

    private void Settingsmenu(InputAction.CallbackContext _)
    {
        Exit();
        ServiceLocator.GetSingleton<InGameMenuUserInterface>().EnterSettings();
    }

    private void Backpackmenu(InputAction.CallbackContext _)
    {
        Exit();
        ServiceLocator.GetSingleton<InGameMenuUserInterface>().EnterBackpack();
    }

    private void Exit()
    {
        _inputActions.Ingame.Disable();
    }
}