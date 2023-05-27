using System;

public class Portrait
{
    public event Action<PortraitSettings> OnUpdated;
    public PortraitSettings Settings { get; private set; }

    public Portrait(PortraitSettings? settings = null)
    {
        var @default = settings ?? PortraitSettings.DEFAULT;
        Set(@default);
    }

    public void Set(PortraitSettings settings)
    {
        Settings = settings;
        OnUpdated?.Invoke(settings);
    }
}
