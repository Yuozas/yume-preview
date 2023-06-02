using System;
using System.Collections.Generic;

public class Toggler : IToggler
{
    public bool Enabled { get; private set; }

    public event Action<bool> OnUpdated;

    public event Action<IToggler> OnEnabled;
    public event Action<IToggler> OnDisabled;

    public event Action OnEnable;
    public event Action OnDisable;

    private readonly Dictionary<bool, Action> _events;

    public Toggler(bool enabled = false)
    {
        _events = new();
        _events.Add(true, InvokeEnabled);
        _events.Add(false, InvokeDisabled);

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

        _events[value].Invoke();
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