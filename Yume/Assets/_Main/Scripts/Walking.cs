﻿using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : IState
{
    private readonly Movement _movement;
    private readonly Direction _direction;
    private readonly Interaction _interaction;
    private readonly InputActions.WalkingActions _walking;

    public Walking(InputActions input, Movement movement, Direction direction, Interaction interaction)
    {
        _walking = input.Walking;

        _movement = movement;
        _direction = direction;
        _interaction = interaction;
    }
    public void Enter()
    {
        _walking.Enable();
        _walking.Interact.performed += Interact;

        _walking.Movement.performed += Move;
        _walking.Movement.canceled += Move;
    }

    public void Exit()
    {
        _walking.Disable();
        _walking.Interact.performed -= Interact;

        _walking.Movement.performed -= Move;
        _walking.Movement.canceled -= Move;
    }

    public void Tick() { }

    private void Interact(InputAction.CallbackContext context)
    {
        _interaction.TryInteract(_movement.Position, _direction.Axis, Interaction.DEFAULT_INTERACTION_DISTANCE);
    }
    private void Move(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        _movement.Tick(axis, Movement.DEFAULT_MOVEMENT_SPEED);
    }
}

