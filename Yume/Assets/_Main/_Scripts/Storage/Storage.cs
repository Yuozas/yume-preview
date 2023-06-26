using Realms;
using System.Linq;

public class Storage : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Name { get; set; }

    [Backlink(nameof(CharacterHasStorage.Storage))]
    public IQueryable<CharacterHasStorage> CharacterHasStorages { get; }

    [Backlink(nameof(StoreHasStorage.Storage))]
    public IQueryable<StoreHasStorage> StoreHasStorages { get; }

    [Backlink(nameof(StorageSlot.Storage))]
    public IQueryable<StorageSlot> StorageSlots { get; }

    [Backlink(nameof(StorageAllowedItemType.Storage))]
    public IQueryable<StorageAllowedItemType> StorageAllowedItemTypes { get; }
}