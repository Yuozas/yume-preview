using System.Collections.Generic;
using System;

public class BaseState
{
    protected readonly Dictionary<Func<bool>, Type> Transitions;
    protected States States;

    public BaseState(Dictionary<Func<bool>, Type> transitions)
    {
        Transitions = transitions;
    }

    public bool TryTransition()
    {
        foreach (var transition in Transitions)
            if (transition.Key())
            {
                States.Set(transition.Value);
                return true;
            }

        return false;
    }

    public void Set(States states)
    {
        States = states;
    }
}