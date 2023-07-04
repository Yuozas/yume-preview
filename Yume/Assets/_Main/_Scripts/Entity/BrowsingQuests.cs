using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using SwiftLocator.Services.ServiceLocatorServices;

public class BrowsingQuests : BaseState, IState
{
    private readonly InputActions.BrowsingQuestsActions _actions;
    private readonly Quests _quests;

    public BrowsingQuests(InputActions.BrowsingQuestsActions actions, Dictionary<Func<bool>, Type> transitions)
       : base(transitions)
    {
        _actions = actions;
        _quests = ServiceLocator.GetSingleton<Quests>();
    }

    public void Enter()
    {
        _actions.Enable();
        _actions.Quit.performed += DisableQuests;
    }

    public void Exit()
    {
        _actions.Disable();
        _actions.Quit.performed -= DisableQuests;
    }

    public void Tick()
    {
        if (TryTransition())
            return;
    }

    private void DisableQuests(InputAction.CallbackContext context)
    {
        _quests.Toggler.Disable();
    }

}