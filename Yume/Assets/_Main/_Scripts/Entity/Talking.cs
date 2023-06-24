using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine.InputSystem;

public class Talking : IState
{
    private readonly InputActions.TalkingActions _talking;
    private readonly DialogueResolver _dialogues;

    private States _states;
    private Dialogue _dialogue;
    private DelayedExecutor _executor;
    private bool _subscribed;

    public Talking(InputActions.TalkingActions actions)
    {
        _talking = actions;
        _dialogues = ServiceLocator.GetSingleton<DialogueResolver>();

        var settings = new DelayedExecutorSettings(1, 0.05f);
        _executor = new DelayedExecutor(settings: settings);
    }

    public void Set(States states)
    {
        _states = states;
    }

    public void Enter()
    {
        // Todo.
        // I'm adding a delay here to subscribing, because there's a bug. When you switch control scheme
        // from walking to talking by pressing the z button, when it switches it call the z button again on the talking scheme,
        // even though it was pressed a second time. Note to self, find why it is happening.

        _subscribed = false;
        _executor.Begin(SubscribeToTalking);

        _dialogue = _dialogues.ResolveWhereTogglerEnabled();
        _dialogue.Disabled += Return;
    }

    public void Exit()
    {
        if (_subscribed)
            UnsubscribeFromTalking();

        _dialogue.Disabled -= Return;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        var dialogue = _dialogues.ResolveWhereTogglerEnabled();
        dialogue.Typewriter.Continue();
    }

    private void Return()
    {
        _states.Set<Walking>();
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