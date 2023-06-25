using System.Collections.Generic;
using System;

public class BaseState
{
    protected readonly Dictionary<Func<bool>, Type> _transitions;
    protected States _states;

    public BaseState(Dictionary<Func<bool>, Type> transitions)
    {
        _transitions = transitions;
    }

    public bool TryTransition()
    {
        foreach (var transition in _transitions)
        {
            var met = transition.Key.Invoke();
            if (!met)
                continue;

            _states.Set(transition.Value);
            return true;
        }

        return false;
    }

    public void Set(States states)
    {
        _states = states;
    }
}