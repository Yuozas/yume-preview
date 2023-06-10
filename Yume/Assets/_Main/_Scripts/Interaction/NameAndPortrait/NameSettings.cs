using System;

[Serializable]
public struct NameSettings
{
    public const string NAME = "Name";
    public static readonly NameSettings Default = new(NAME);

    public string Name;

    public NameSettings(string name)
    {
        Name = name;
    }
}

