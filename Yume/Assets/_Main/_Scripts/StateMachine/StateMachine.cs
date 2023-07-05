using System.Linq;
using System;

public class StateMachine
{
    private IState _current;
    private readonly IState[] _states;

    public StateMachine(IState[] states)
    {
        _states = states;
        foreach (var state in _states)
            state.SetReferenceToStateMachine(this);
    }

    public void SetState(IState state)
    {
        _current?.Exit();

        _current = state;
        _current?.Enter();
    }

    public void SetState(Type type)
    {
        var state = _states.First(state => state.GetType() == type);
        SetState(state);
    }

    public void SetState<T>() where T : IState
    {
        var state = _states.OfType<T>().First();
        SetState(state);
    }

    public void Tick()
    {
        _current?.Tick();
    }
}