using SwiftLocator.Services.ServiceLocatorServices;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Talking : BaseState, IState
{
    private readonly InputActions.TalkingActions _talking;
    private readonly DialogueResolver _dialogues;

    private readonly DelayedExecutor _executor;
    private bool _subscribed;

    public Talking(InputActions.TalkingActions actions, Dictionary<Func<bool>, Type> transitions) : base(transitions)
    {
        _talking = actions;
        _dialogues = ServiceLocator.GetSingleton<DialogueResolver>();

        var settings = new DelayedExecutorSettings(1, 0.05f);
        _executor = new DelayedExecutor(settings: settings);
    }

    public void Enter()
    {
        _subscribed = false;
        _executor.Begin(SubscribeToTalking);
    }

    public void Tick()
    {
        _ = TryTransition();
    }

    public void Exit()
    {
        if (_subscribed)
            UnsubscribeFromTalking();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        var dialogue = _dialogues.ResolveWhereTogglerEnabled();
        dialogue.Typewriter.Continue();
    }

    private void UnsubscribeFromTalking()
    {
        _talking.Disable();
        _talking.Interact.performed -= Interact;
    }

    private void SubscribeToTalking()
    {
        _talking.Enable();
        _talking.Interact.performed += Interact;

        _subscribed = true;
    }
}