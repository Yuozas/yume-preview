using MongoDB.Bson;
using Realms;
using System.Linq;

public class Storage : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string Name { get; set; }

    [Backlink(nameof(CharacterHasStorage.Storage))]
    public IQueryable<CharacterHasStorage> CharacterHasStorages { get; }

    [Backlink(nameof(StoreHasStorage.Storage))]
    public IQueryable<StoreHasStorage> StoreHasStorages { get; }

    [Backlink(nameof(StorageSlot.Storage))]
    public IQueryable<StorageSlot> StorageSlots { get; }
}