using Realms;
using System.Linq;

public class CharacterRealmObject : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; }
    public CharacterTypeRealmObject Type { get; set; }
    public string Name { get; set; }
    public string SceneName { get; set; }

    [Backlink(nameof(CharacterHasStorage.Character))]
    public IQueryable<CharacterHasStorage> CharacterHasStorages { get; }
}