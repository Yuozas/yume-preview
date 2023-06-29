using Realms;
using System.Linq;
using MongoDB.Bson;

public class Store : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string Name { get; set; }

    [Backlink(nameof(StoreHasStorage.Store))]
    public IQueryable<StoreHasStorage> Storages { get; }
}
