using System;
using UnityEngine;

[Serializable]
public class Name
{
    public event Action<NameSettings> OnUpdated;
    [field: SerializeField] public NameSettings Settings { get; private set; }

    public Name(NameSettings? settings = null)
    {
        Set(settings);
    }

    public void Set(NameSettings? settings = null)
    {
        var @default = settings ?? NameSettings.Default;
        Settings = @default;
        OnUpdated?.Invoke(@default);
    }
}

