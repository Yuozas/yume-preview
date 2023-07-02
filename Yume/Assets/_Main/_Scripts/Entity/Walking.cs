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
        if (TryTransition())
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

public class PlayingSliderGame : BaseState, IState
{
    private readonly InputActions.SliderActions _slider;
    private readonly SliderGame _sliderGame;

    public PlayingSliderGame(InputActions.SliderActions actions, Dictionary<Func<bool>, Type> transitions) : base(transitions)
    {
        _slider = actions;
        _sliderGame = ServiceLocator.GetSingleton<SliderGame>();
    }

    public void Enter()
    {
        _slider.Enable();
        _slider.Interact.performed += Interact;
    }

    public void Exit()
    {
        _slider.Disable();
        _slider.Interact.performed -= Interact;
    }

    public void Tick()
    {
        if (TryTransition())
            return;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _sliderGame.Execute();
    }
}