using System.Collections.Generic;
using System;

public class BaseState
{
    protected readonly Dictionary<Func<bool>, Type> Transitions;
    protected StateMachine StateMachine;

    public BaseState(Dictionary<Func<bool>, Type> transitions)
    {
        Transitions = transitions;
    }

    public bool TryTransitionToAnotherState()
    {
        foreach (var transition in Transitions)
            if (transition.Key())
            {
                StateMachine.SetState(transition.Value);
                return true;
            }

        return false;
    }

    public void SetReferenceToStateMachine(StateMachine states)
    {
        StateMachine = states;
    }
}