using UnityEngine;
using UnityEngine.InputSystem;
using Euphelia;

public class Walking : IState
{
    private readonly Movement _movement;
    private readonly Direction _direction;
    private readonly Interactor _interaction;
    private readonly InputActions.WalkingActions _walking;

    public Walking(InputActions.WalkingActions actions, Movement movement, Direction direction, Interactor interaction)
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
        _movement.Tick(Movement.DEFAULT_SPEED);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _interaction.TryInteract(_movement.Position, _direction.Axis, Interactor.DEFAULT_INTERACTION_DISTANCE);
    }

    private void SetAxis(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        _movement.Set(axis);
    }
}

