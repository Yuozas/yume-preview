using System;
using UnityEngine;

[Serializable]
public class Portrait
{
    public event Action<PortraitSettings> OnUpdated;
    [field: SerializeField] public PortraitSettings Settings { get; private set; }

    public Portrait(PortraitSettings? settings = null)
    {
        Set(settings);
    }

    public void Set(PortraitSettings? settings = null)
    {
        var @default = settings ?? PortraitSettings.Default;
        Settings = @default;
        OnUpdated?.Invoke(@default);
    }
}
