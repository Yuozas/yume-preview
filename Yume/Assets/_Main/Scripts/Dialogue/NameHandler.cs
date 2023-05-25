using System;

public class NameHandler
{
    public const string DEFAULT = "Name";
    public event Action<string> OnUpdated;
    public string Text { get; private set; }

    public NameHandler(string text = DEFAULT)
    {
        Set(text);
    }

    public void Set(string text)
    {
        Text = text;
        OnUpdated?.Invoke(text);
    }
}
