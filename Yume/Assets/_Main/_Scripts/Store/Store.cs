using Realms;
using System.Linq;
using System;

public class Store : RealmObject
{
    [PrimaryKey]
    public long Id { get; set; } = DateTime.UtcNow.Ticks;
    public string Name { get; set; }

    [Backlink(nameof(StoreHasStorage.Store))]
    public IQueryable<StoreHasStorage> Storages { get; }
}
