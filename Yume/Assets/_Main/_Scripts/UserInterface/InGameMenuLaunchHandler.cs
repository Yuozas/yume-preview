using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenuLaunchHandler : MonoBehaviour
{
    private InputActions _inputActions;

    private DialogueResolver _dialogueResolver;
    private Decisions _decisions;
    private SliderGame _slider;
    private Quests _quests;
    private StoreUserInterface _store;

    public void Enter()
    {
        _inputActions.Ingame.Enable();
        _inputActions.Ingame.Backpackmenu.performed += Backpackmenu;
        _inputActions.Ingame.Settingsmenu.performed += Settingsmenu;
    }

    private void Awake()
    {
        _decisions = ServiceLocator.GetSingleton<Decisions>();
        _dialogueResolver = ServiceLocator.GetSingleton<DialogueResolver>();
        _slider = ServiceLocator.GetSingleton<SliderGame>();
        _quests = ServiceLocator.GetSingleton<Quests>();
        _store = ServiceLocator.GetSingleton<StoreUserInterface>();

        _inputActions = new InputActions();
        Enter();
    }

    private void Settingsmenu(InputAction.CallbackContext _)
    {
        var shouldOpen = ShouldOpen();
        if (!shouldOpen)
            return;

        Exit();
        ServiceLocator.GetSingleton<InGameMenuUserInterface>().EnterSettings();
    }

    private void Backpackmenu(InputAction.CallbackContext _)
    {
        var shouldOpen = ShouldOpen();
        if (!shouldOpen)
            return;

        Exit();
        ServiceLocator.GetSingleton<InGameMenuUserInterface>().EnterBackpack();
    }

    private bool ShouldOpen()
    {
        var dialogues = _dialogueResolver.Resolve();
        return dialogues.All(dialogue => !dialogue.Toggler.Enabled) && !_decisions.Toggler.Enabled && !_slider.Toggler.Enabled && !_quests.Toggler.Enabled && !_store.Active;
    }

    private void Exit()
    {
        _inputActions.Ingame.Disable();
    }
}