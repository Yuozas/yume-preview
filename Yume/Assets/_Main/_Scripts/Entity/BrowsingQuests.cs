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
        _actions.Quit.performed += HideQuestsUserInterfaceWindow;
        _actions.Next.performed += SelectNextQuest;
        _actions.Previous.performed += SelectPreviousQuest;
    }

    public void Exit()
    {
        _actions.Disable();
        _actions.Quit.performed -= HideQuestsUserInterfaceWindow;
        _actions.Next.performed -= SelectNextQuest;
        _actions.Previous.performed -= SelectPreviousQuest;
    }

    public void Tick()
    {
        if (TryTransitionToAnotherState())
            return;
    }

    private void SelectNextQuest(InputAction.CallbackContext context)
    {
        _quests.Next();
    }

    private void SelectPreviousQuest(InputAction.CallbackContext context)
    {
        _quests.Previous();
    }

    private void HideQuestsUserInterfaceWindow(InputAction.CallbackContext context)
    {
        _quests.Toggler.Disable();
    }
}