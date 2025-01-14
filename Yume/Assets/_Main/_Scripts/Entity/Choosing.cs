﻿using SwiftLocator.Services.ServiceLocatorServices;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Choosing : BaseState, IState
{
    private readonly InputActions.ChoosingActions _choosing;
    private readonly ChoiceGroup _choices;
    private readonly DelayedExecutor _executor;

    private bool _subscribed;

    public Choosing(InputActions.ChoosingActions actions, Dictionary<Func<bool>, Type> transitions) : base(transitions)
    {
        _choosing = actions;
        _choices = ServiceLocator.GetSingleton<Decisions>().Choices;

        _executor = new DelayedExecutor(settings: DelayedExecutorSettings.Default);
    }

    public void Enter()
    {
        _subscribed = false;
        _executor.Begin(Subscribe);
    }

    private void Subscribe()
    {
        _choosing.Enable();
        _choosing.Interact.performed += Interact;
        _choosing.Next.performed += Next;
        _choosing.Previous.performed += Previous;

        _subscribed = true;
    }

    public void Tick()
    {
        _ = TryTransitionToAnotherState();
    }

    public void Exit()
    {
        if(_subscribed)
            Unsubscribe();
    }

    private void Unsubscribe()
    {
        _choosing.Disable();
        _choosing.Interact.performed -= Interact;
        _choosing.Next.performed -= Next;
        _choosing.Previous.performed -= Previous;

        _subscribed = false;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _choices.Choose();
    }

    private void Next(InputAction.CallbackContext context)
    {
        _choices.Next();
    }

    private void Previous(InputAction.CallbackContext context)
    {
        _choices.Previous();
    }
}