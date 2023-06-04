using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine.InputSystem;

public class Talking : IState
{
    private readonly InputActions.TalkingActions _talking;
    private readonly DialogueGroup _dialogues;
    private States _states;

    public Talking(InputActions.TalkingActions actions)
    {
        _talking = actions;
    }

    public void Set(States states)
    {
        _states = states;
    }
    public void Enter()
    {
        _talking.Enable();
        _talking.Interact.performed += Interact;
        Dialogue.Disabled += Return;
    }
    public void Exit()
    {
        _talking.Disable();
        _talking.Interact.performed -= Interact;
        Dialogue.Disabled -= Return;
    }
    private void Interact(InputAction.CallbackContext context)
    {
        var dialogue = _dialogues.GetEnabled();
        dialogue.Typewriter.Continue();
    }
    private void Return()
    {
        var type = typeof(Walking);
        _states.Set(type);
    }

    public void Tick() { }
}

