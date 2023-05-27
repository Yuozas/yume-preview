using System;
using System.Collections.Generic;

public class Toggle : IToggle
{
    public bool Enabled { get; private set; }

    public event Action<bool> OnUpdated;

    public event Action<IToggle> OnEnabled;
    public event Action<IToggle> OnDisabled;

    public event Action OnEnable;
    public event Action OnDisable;

    private readonly Dictionary<bool, Action> _actions;

    public Toggle(bool enabled = false)
    {
        _actions = new();
        _actions.Add(true, InvokeEnabled);
        _actions.Add(false, InvokeDisabled);

        Set(enabled);
    }

    public void Enable()
    {
        Set(true);
    }

    public void SetOpposite()
    {
        Set(!Enabled);
    }

    public void Disable()
    {
        Set(false);
    }

    private void Set(bool value)
    {
        Enabled = value;
        OnUpdated?.Invoke(value);

        _actions[value].Invoke();
    }

    private void InvokeEnabled()
    {
        OnEnable?.Invoke();
        OnEnabled?.Invoke(this);
    }

    private void InvokeDisabled()
    {
        OnDisable?.Invoke();
        OnDisabled?.Invoke(this);
    }
}