using System;

public class Portrait
{
    public event Action<PortraitSettings> OnUpdated;
    public PortraitSettings Settings { get; private set; }

    public Portrait(PortraitSettings? settings = null)
    {
        Set(settings);
    }

    public void Set(PortraitSettings? settings = null)
    {
        var @default = settings ?? PortraitSettings.DEFAULT;
        Settings = @default;
        OnUpdated?.Invoke(@default);
    }
}
