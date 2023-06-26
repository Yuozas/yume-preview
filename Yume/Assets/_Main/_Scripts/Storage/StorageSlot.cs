using Realms;
using System;
using System.Linq;

public class StorageSlot : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; } = DateTime.UtcNow.Ticks;
    public Storage Storage { get; set; }
    public Item Item { get; set; }

    [Backlink(nameof(StorageSlotHasPrice.StorageSlot))]
    public IQueryable<StorageSlotHasPrice> Prices { get; }
}