using System.Collections.Generic;

public readonly struct Character
{
    public static readonly Character Ember = new("0", CharacterType.Main, "Ember", Scene.DemoScene.Name);
    public static readonly Character Aura = new("1", CharacterType.Main, "Aura", Scene.DemoScene.Name);
    public static readonly Character Hazel = new("2", CharacterType.Support, "Hazel");
    public static readonly Character Nova = new("3", CharacterType.Support, "Nova");
    public static readonly Character Clar = new("4", CharacterType.Support, "Clar");

    public static Dictionary<string, Character> AllCharacters { get; } = ReflectionUtility.GetStaticFieldDictionaryWithId<Character>();

    public string Id { get; }
    public CharacterType Type { get; }
    public string Name { get; }
    public string SceneName { get; }

    private Character(string id, CharacterType type, string name, string sceneName)
        : this(id, type, name)
    {
        SceneName = sceneName;
    }

    private Character(string id, CharacterType type, string name)
    {
        Id = id;
        Type = type;
        Name = name;
        SceneName = string.Empty;
    }

    public static implicit operator CharacterRealmObject(Character character) => new()
    {
        Id = character.Id,
        Name = character.Name,
        SceneName = character.SceneName,
        CharacterType = character.Type
    };
}
