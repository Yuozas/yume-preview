using System;

public class Name
{
    public event Action<NameSettings> OnUpdated;
    public NameSettings Settings { get; private set; }

    public Name(NameSettings? text = null)
    {
        var @default = text ?? NameSettings.DEFAULT;
        Set(@default);
    }

    public void Set(NameSettings text)
    {
        Settings = text;
        OnUpdated?.Invoke(text);
    }
}

