using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class InGameMenuLaunchHandler : MonoBehaviour
{
    private InputActions _inputActions;

    public void Awake()
    {
        _inputActions = new InputActions();
        Enter();
    }

    public void Enter()
    {
        _inputActions.Ingame.Enable();
        _inputActions.Ingame.Backpackmenu.performed += _ =>
        {
            ServiceLocator.GetSingleton<InGameMenuUserInterface>().EnterBackpack();
            Exit();
        };
        _inputActions.Ingame.Settingsmenu.performed += _ =>
        {
            ServiceLocator.GetSingleton<InGameMenuUserInterface>().EnterSettings();
            Exit();
        };
    }

    public void Exit()
    {
        _inputActions.Ingame.Disable();
    }
}