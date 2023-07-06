using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using SwiftLocator.Services.ServiceLocatorServices;

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
        if (TryTransitionToAnotherState())
            return;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _sliderGame.Execute();
    }
}