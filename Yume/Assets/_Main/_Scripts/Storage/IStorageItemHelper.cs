using Realms;

public interface IStorageItemHelper
{
    bool TryAddItemToBackpack(Item item, int slotIndex);
    bool TryAddItemToStorage(Item item, int slotIndex, Storage storage);
    bool TryAddItemToStorage(Item item, int slotIndex, Storage storage, Realm activeRealm);
}