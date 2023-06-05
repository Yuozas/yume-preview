using System;

public class States
{
    private IState _current;
    private IState[] _states;

    public States(IState[] states)
    {
        _states = states;
    }
    public void Set(IState state)
    {
        _current?.Exit();

        _current = state;
        _current.Enter();
    }

    public void Set(Type type)
    {
        for (int i = 0; i < _states.Length; i++)
        {
            var state = _states[i];
            var target = state.GetType();
            if (target == type)
            {
                Set(state);
                return;
            }
        }
    }

    public void Tick()
    {
        _current.Tick();
    }
}