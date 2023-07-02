using Realms;

public class CharacterHasStorage : RealmObject
{
    public CharacterRealmObject Character { get; set; }
    public Storage Storage { get; set; }
}