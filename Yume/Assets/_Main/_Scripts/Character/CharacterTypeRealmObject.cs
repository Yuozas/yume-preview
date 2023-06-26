using Realms;
using System.Linq;

public class CharacterTypeRealmObject : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Name { get; set; }

    [Backlink(nameof(CharacterRealmObject.Type))]
    public IQueryable<CharacterRealmObject> Characters { get; }
}