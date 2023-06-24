﻿using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine.InputSystem;

public class Choosing : IState
{
    private readonly InputActions.ChoosingActions _choosing;
    private readonly ChoiceGroup _choices;

    public Choosing(InputActions.ChoosingActions actions)
    {
        _choosing = actions;
        _choices = ServiceLocator.GetSingleton<Decisions>().Choices;
    }

    public void Enter()
    {
        _choosing.Enable();
        _choosing.Interact.performed += Interact;
        _choosing.Next.performed += Next;
        _choosing.Previous.performed += Previous;
    }

    public void Exit()
    {
        _choosing.Disable();
        _choosing.Interact.performed -= Interact;
        _choosing.Next.performed -= Next;
        _choosing.Previous.performed -= Previous;
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