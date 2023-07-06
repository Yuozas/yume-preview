using Realms;

public interface ICreateStorageHelper
{
    void CreateStorageForCharacter(Realm realm, StorageScriptableObject storage);
    void CreateStorage(Realm realm, StorageScriptableObject storage);
    void CreateStorage(Realm realm, Storage storage);
    void CreateSlots(Realm relam, params StorageSlot[] storageSlots);
    void CreateSlotPrices(Realm realm, params StorageSlotHasPrice[] storageSlotHasPrices);
}