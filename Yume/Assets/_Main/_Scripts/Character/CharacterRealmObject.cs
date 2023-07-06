using Realms;
using System.Linq;

public class CharacterRealmObject : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }
    public int Type { get; set; }
    public string Name { get; set; }
    public string SceneName { get; set; }
    public float PossesedMoney { get; set; }

    [Ignored]
    public CharacterType CharacterType
    {
        get => (CharacterType)Type;
        set => Type = (int)value;
    }

    [Backlink(nameof(CharacterHasStorage.Character))]
    public IQueryable<CharacterHasStorage> CharacterHasStorages { get; }
}