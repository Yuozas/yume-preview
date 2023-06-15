using System;

public readonly struct DefaultCharacterData
{
    public const string MAIN_TYPE = "main";
    public const string SUPPORT_TYPE = "support";

    public DefaultCharacterData(int id, DefaultCharacterData character) 
        : this(id, character.Type, character.Name, character.SceneName)
    {
    }

    public DefaultCharacterData(string type, string name, string sceneName = null) 
        : this(-1, type, name, sceneName)
    {
    }

    public DefaultCharacterData(int id, string type, string name, string sceneName)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Character type cannot be null or white space.");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Character name cannot be null or white space.");

        Id = id;
        Type = type;
        Name = name;
        SceneName = sceneName;
    }

    public readonly int Id;
    public readonly string Type;
    public readonly string Name;
    public readonly string SceneName;
}