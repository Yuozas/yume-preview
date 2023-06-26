using System.Collections.Generic;

public readonly struct CharacterType
{
    public static readonly CharacterType MainType = new(0, "Main");
    public static readonly CharacterType SupportType = new(1, "Support");

    public static Dictionary<long, CharacterType> AllTypes { get; } = ReflectionUtility.GetStaticFieldDictionaryWithId<CharacterType>();

    private CharacterType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; }
    public string Name { get; }

    public static implicit operator CharacterTypeRealmObject(CharacterType characterType) => new()
    {
        Id = characterType.Id,
        Name = characterType.Name
    };
}