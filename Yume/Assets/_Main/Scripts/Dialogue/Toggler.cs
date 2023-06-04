using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Toggler : IToggler
{
    [field: SerializeField] public bool Enabled { get; private set; }

    public event Action<bool> OnUpdated;

    public event Action<IToggler> OnEnabled;
    public event Action<IToggler> OnDisabled;

    public event Action OnEnable;
    public event Action OnDisable;

    public Toggler(bool enabled = false)
    {
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

        if (value)
            InvokeEnabled();
        else
            InvokeDisabled();
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