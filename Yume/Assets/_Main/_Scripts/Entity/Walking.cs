using UnityEngine;
using UnityEngine.InputSystem;
using Euphelia;
using System.Collections.Generic;
using System;
using SwiftLocator.Services.ServiceLocatorServices;

public class Walking : BaseState, IState
{
    private readonly Movement _movement;
    private readonly Direction _direction;
    private readonly IInteractor _interaction;
    private readonly InputActions.WalkingActions _walking;
    private readonly Quests _quests;

    public Walking(InputActions.WalkingActions actions, Movement movement, Direction direction, IInteractor interaction, Dictionary<Func<bool>, Type> transitions) 
       : base(transitions)
    {
        _walking = actions;

        _movement = movement;
        _direction = direction;
        _interaction = interaction;

        _quests = ServiceLocator.GetSingleton<Quests>();
    }

    public void Enter()
    {
        _walking.Enable();

        _walking.Interact.performed += Interact;
        _walking.Movement.performed += SetAxis;
        _walking.Movement.canceled += SetAxis;
        _walking.Quests.performed += EnableQuests;
    }

    public void Exit()
    {
        _walking.Disable();

        _walking.Interact.performed -= Interact;
        _walking.Movement.performed -= SetAxis;
        _walking.Movement.canceled -= SetAxis;
        _walking.Quests.performed -= EnableQuests;

        _movement.Stop();
    }

    public void Tick()
    {
        if (TryTransition())
            return;

        _movement.Tick(Movement.DEFAULT_SPEED);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _interaction.TryInteract(_movement.Position, _direction.Axis, IInteractor.DEFAULT_DISTANCE);
    }

    private void EnableQuests(InputAction.CallbackContext context)
    {
        _quests.Toggler.Enable();
    }

    private void SetAxis(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        _movement.Set(axis);
    }
}