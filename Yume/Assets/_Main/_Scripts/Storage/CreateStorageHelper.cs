using Realms;
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
            realm.Add(new StorageSlot { Storage = storage });

        if(slotsToAdd < 0)
        {
            var slotsToRemove = currentStorageSlotCount - storage.Slots;
            var slotsToRemoveFromStorage = storageInDb.StorageSlots.Take(slotsToRemove);

            realm.RemoveRange(slotsToRemoveFromStorage);
        }
    }
}