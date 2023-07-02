using Realms;

public interface IStorageItemHelper
{
    bool TryAddItemToBackpack(Item item);
    bool TryAddItemToBackpack(Item item, int slotIndex);
    void MoveFromTo(string storageIdFrom, string storageIdTo, Item item);
    bool TryAddItemToStorage(Item item, int slotIndex, Storage storage, Realm activeRealm);
}