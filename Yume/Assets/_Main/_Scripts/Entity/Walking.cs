﻿using UnityEngine;
using UnityEngine.InputSystem;
using Euphelia;
using System.Collections.Generic;
using System;

public class Walking : BaseState, IState
{
    private readonly Movement _movement;
    private readonly Direction _direction;
    private readonly IInteractor _interaction;
    private readonly InputActions.WalkingActions _walking;

    public Walking(InputActions.WalkingActions actions, Movement movement, Direction direction, IInteractor interaction, Dictionary<Func<bool>, Type> transitions) 
       : base(transitions)
    {
        _walking = actions;

        _movement = movement;
        _direction = direction;
        _interaction = interaction;
    }

    public void Enter()
    {
        _walking.Enable();

        _walking.Interact.performed += Interact;
        _walking.Movement.performed += SetAxis;
        _walking.Movement.canceled += SetAxis;
    }

    public void Exit()
    {
        _walking.Disable();

        _walking.Interact.performed -= Interact;
        _walking.Movement.performed -= SetAxis;
        _walking.Movement.canceled -= SetAxis;

        _movement.Stop();
    }

    public void Tick()
    {
        var met = TryTransition();
        if (met)
            return;

        _movement.Tick(Movement.DEFAULT_SPEED);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _interaction.TryInteract(_movement.Position, _direction.Axis, IInteractor.DEFAULT_DISTANCE);
    }

    private void SetAxis(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        _movement.Set(axis);
    }
}