using Realms;
using System.Collections.Generic;

public class StorageAllowedItemTypes : RealmObject
{
    public Storage Storage { get; set; }
    public IList<ItemTypeRealmObject> ItemTypes { get; }
}
