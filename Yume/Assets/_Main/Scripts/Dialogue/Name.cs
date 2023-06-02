using System;

public class Name
{
    public event Action<NameSettings> OnUpdated;
    public NameSettings Settings { get; private set; }

    public Name(NameSettings? settings = null)
    {
        Set(settings);
    }

    public void Set(NameSettings? settings = null)
    {
        var @default = settings ?? NameSettings.DEFAULT;
        Settings = @default;
        OnUpdated?.Invoke(@default);
    }
}

