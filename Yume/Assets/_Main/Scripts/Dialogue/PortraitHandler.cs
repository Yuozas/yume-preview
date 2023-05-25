using System;

public class PortraitHandler
{
    public event Action<PortraitHandlerSettings> OnUpdated;
    public PortraitHandlerSettings Settings { get; private set; }

    public PortraitHandler(PortraitHandlerSettings? settings = null)
    {
        var @default = settings ?? PortraitHandlerSettings.DEFAULT;
        Set(@default);
    }

    public void Set(PortraitHandlerSettings settings)
    {
        Settings = settings;
        OnUpdated?.Invoke(settings);
    }
}
