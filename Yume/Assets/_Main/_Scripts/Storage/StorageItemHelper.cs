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

    public bool TryAddItemToStorage(Item item, Storage storage)
    {
        if (item is null)
            return false;

        using var activeRealm = _realmActiveSaveHelper.GetActiveSave();

        var firstEmptySlot = storage.StorageSlots.FirstOrDefault(s => s.Item == null);
        if (firstEmptySlot is null)
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

    public bool TryMoveFromTo(string storageSlotIdFrom, string storageIdTo, Item item)
    {
        using var realm = _realmActiveSaveHelper.GetActiveSave();
        using var transaction = realm.StartTransaction();

        if(!TryMoveFromTo(storageSlotIdFrom, storageIdTo, item, realm))
        {
            transaction.Rollback();
            return false;
        }

        transaction.Commit();
        return true;
    }

    public bool TryMoveFromTo(string storageSlotIdFrom, string storageIdTo, Item item, Realm realm)
    {
        if (!realm.TryGet<Storage>(storageIdTo, out var storageTo))
            throw new ArgumentException($"Invalid {storageIdTo} passed.");

        if (!realm.TryGet<StorageSlot>(storageSlotIdFrom, out var fromSlot))
            throw new ArgumentException($"Invalid {storageSlotIdFrom} passed.");

        if (fromSlot.Item is null)
            throw new ArgumentException($"Invalid slot passed no item.");

        fromSlot.Item = null;

        return TryAddItemToStorage(item, storageTo);
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

        activeRealm.WriteSafe(() =>
        {
            activeRealm.Add(item, true);
            slot.Item = item;
        });

        return true;
    }
}