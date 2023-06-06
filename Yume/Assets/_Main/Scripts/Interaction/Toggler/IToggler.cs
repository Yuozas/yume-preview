using System;

public interface IToggler
{
    bool Enabled { get; }

    event Action<IToggler> OnEnabled;
    event Action<IToggler> OnDisabled;
    event Action<bool> OnUpdated;

    event Action OnEnable;
    event Action OnDisable;

    void SetOpposite();
    void Enable();
    void Disable();
}

