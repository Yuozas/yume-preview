using Realms;
using System.Linq;

public class Item : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string IconPath { get; set; }

    [Backlink(nameof(StorageSlot.Item))]
    public IQueryable<StorageSlot> StorageSlots { get; }
}
