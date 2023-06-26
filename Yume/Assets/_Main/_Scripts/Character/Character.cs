using System.Collections.Generic;

public readonly struct Character
{
    public static readonly Character Ember = new(0, CharacterType.MainType, "Ember", Scene.DemoScene.Name);
    public static readonly Character Aura = new(1, CharacterType.MainType, "Aura", Scene.DemoScene.Name);
    public static readonly Character Hazel = new(2, CharacterType.MainType, "Hazel");
    public static readonly Character Nova = new(3, CharacterType.MainType, "Nova");
    public static readonly Character Clar = new(4, CharacterType.MainType, "Clar");

    public static Dictionary<long, Character> AllCharacters { get; } = ReflectionUtility.GetStaticFieldDictionaryWithId<Character>();

    private Character(long id, CharacterType type, string name, string sceneName)
        : this(id, type, name)
    {
        SceneName = sceneName;
    }

    private Character(long id, CharacterType type, string name)
    {
        Id = id;
        Type = type;
        Name = name;
        SceneName = string.Empty;
    }

    public long Id { get; }
    public CharacterType Type { get; }
    public string Name { get; }
    public string SceneName { get; }

    public static implicit operator CharacterRealmObject(Character character) => new()
    {
        Id = character.Id,
        Type = character.Type,
        Name = character.Name,
        SceneName = character.SceneName
    };
}
