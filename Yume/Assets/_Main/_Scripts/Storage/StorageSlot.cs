using MongoDB.Bson;
using Realms;
using System.Linq;

public class StorageSlot : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public Storage Storage { get; set; }
    public Item Item { get; set; }

    [Backlink(nameof(StorageSlotHasPrice.StorageSlot))]
    public IQueryable<StorageSlotHasPrice> Prices { get; }
}