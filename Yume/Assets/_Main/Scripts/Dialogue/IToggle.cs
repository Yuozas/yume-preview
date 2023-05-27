using System;

public interface IToggle
{
    bool Enabled { get; }

    event Action<IToggle> OnEnabled;
    event Action<IToggle> OnDisabled;
    event Action<bool> OnUpdated;

    event Action OnEnable;
    event Action OnDisable;

    void SetOpposite();
    void Enable();
    void Disable();
}

