using Realms;

public class ItemTypeRealmObject : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Name { get; set; }
}