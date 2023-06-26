using Realms;
using System.Linq;

public class Item : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public byte[] ItemIcon { get; set; }

    [Backlink(nameof(StorageSlot.Item))]
    public IQueryable<StorageSlot> StorageSlots { get; }
}
