using Realms;
using System.Linq;

public class StorageItemHelper : IStorageItemHelper
{
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;

    public StorageItemHelper(IRealmActiveSaveHelper realmActiveSaveHelper)
    {
        _realmActiveSaveHelper = realmActiveSaveHelper;
    }

    public bool TryAddItemToBackpack(Item item, int slotIndex)
    {
        using var activeRealm = _realmActiveSaveHelper.GetActiveSave();
        var storage = activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage;
        return TryAddItemToStorage(item, slotIndex, storage, activeRealm);
    }

    public bool TryAddItemToStorage(Item item, int slotIndex, Storage storage)
    {
        using var activeRealm = _realmActiveSaveHelper.GetActiveSave();
        return TryAddItemToStorage(item, slotIndex, storage, activeRealm);
    }

    public bool TryAddItemToStorage(Item item, int slotIndex, Storage storage, Realm activeRealm)
    {
        var slot = storage.StorageSlots.Skip(slotIndex).FirstOrDefault();

        if (slot is null)
            return false;

        if (slot.Item is not null)
            return false;

        var allowedAnyType = storage.StorageAllowedItemTypes.Count() is 0;
        if (!allowedAnyType && storage.StorageAllowedItemTypes.Any(itemType => itemType.ItemType != item.Type))
            return false;


        activeRealm.Write(() =>
        {
            activeRealm.Add(item, true);
            slot.Item = item;
        });

        return true;
    }
}