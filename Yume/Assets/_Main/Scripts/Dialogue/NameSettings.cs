public struct NameSettings
{
    public const string NAME = "Name";
    public static readonly NameSettings DEFAULT = new(NAME);

    public string Name;

    public NameSettings(string name)
    {
        Name = name;
    }
}

