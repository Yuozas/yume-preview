using Realms;
using System;
using System.Linq;

public class CreateStorageHelper : ICreateStorageHelper
{
    public void CreateStorageForCharacter(Realm realm, StorageScriptableObject storage)
    {
        CreateStorage(realm, storage);

        var activeCharaceter = realm.Get<ActiveCharacer>().Character;

        realm.Add(new CharacterHasStorage
        {
            Character = activeCharaceter,
            Storage = storage
        }, true);
    }

    public void CreateStorage(Realm realm, StorageScriptableObject storage)
    {
        var storageInDb = realm.Add<Storage>(storage, true);

        var currentStorageSlotCount = storageInDb.StorageSlots.Count();
        var slotsToAdd = storage.Slots - currentStorageSlotCount;

        for (var i = 0; i < slotsToAdd; i++)
            realm.Add(new StorageSlot { Storage = storageInDb });

        if(slotsToAdd < 0)
        {
            var slotsToRemove = currentStorageSlotCount - storage.Slots;
            var slotsToRemoveFromStorage = storageInDb.StorageSlots.Take(slotsToRemove);

            realm.RemoveRange(slotsToRemoveFromStorage);
        }
    }

    public void CreateStorage(Realm realm, Storage storage)
    {
        realm.Add(storage, true);
    }

    public void CreateSlots(Realm relam, params StorageSlot[] storageSlots)
    {
        if (storageSlots is not { Length: > 0 })
            throw new ArgumentException("No storage slots pasesed.");
        foreach (var slot in storageSlots)
            relam.Add(slot);
    }

    public void CreateSlotPrices(Realm realm, params StorageSlotHasPrice[] storageSlotHasPrices)
    {
        if (storageSlotHasPrices is not { Length: > 0 })
            throw new ArgumentException("No storage slot prices passed.");
        foreach (var slotPrice in storageSlotHasPrices)
            realm.Add(slotPrice);
    }
}