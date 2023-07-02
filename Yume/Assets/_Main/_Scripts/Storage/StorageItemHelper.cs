using Realms;
using System;
using System.Linq;

public class StorageItemHelper : IStorageItemHelper
{
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;

    public StorageItemHelper(IRealmActiveSaveHelper realmActiveSaveHelper)
    {
        _realmActiveSaveHelper = realmActiveSaveHelper;
    }

    public bool TryAddItemToBackpack(Item item)
    {
        if(item is null)
            return false;

        using var activeRealm = _realmActiveSaveHelper.GetActiveSave();

        var storage = GetStorage(activeRealm);

        var firstEmptySlot = storage.StorageSlots.FirstOrDefault(s => s.Item == null);
        if(firstEmptySlot is null)
            return false;

        return TryAddItemToStorage(item, firstEmptySlot, activeRealm);
    }

    public bool TryAddItemToBackpack(Item item, int slotIndex)
    {
        if (item is null)
            return false;

        using var activeRealm = _realmActiveSaveHelper.GetActiveSave();
        var storage = activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage;
        return TryAddItemToStorage(item, slotIndex, storage, activeRealm);
    }

    public bool TryAddItemToStorage(Item item, int slotIndex, Storage storage, Realm activeRealm)
    {
        if (item is null)
            return false;

        var slot = storage.StorageSlots.Skip(slotIndex).FirstOrDefault();
        return TryAddItemToStorage(item, slot, activeRealm);
    }

    public void MoveFromTo(string storageIdFrom, string storageIdTo, Item item)
    {
        using var realm = _realmActiveSaveHelper.GetActiveSave();
        var updatedStorageTo = realm.TryWriteUpdate<StorageSlot>(storageIdTo, to =>
        {
            if(!realm.TryGet<StorageSlot>(storageIdFrom, out var from))
                throw new ArgumentException($"Invalid {storageIdFrom}");
            else if(from.Item.Id != item.Id)
                throw new ArgumentException($"Invalid {item.Id}");

            from.Item = null;
            to.Item = item;
        });

        if (!updatedStorageTo)
            throw new ArgumentException($"Invalid {storageIdTo}");
    }

    private Storage GetStorage(Realm activeRealm)
    {
        return activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage;
    }

    private bool TryAddItemToStorage(Item item, StorageSlot slot, Realm activeRealm)
    {
        if (item is null)
            return false;

        if (slot is null)
            return false;

        if (slot.Item is not null)
            return false;

        activeRealm.Write(() =>
        {
            activeRealm.Add(item, true);
            slot.Item = item;
        });

        return true;
    }
}