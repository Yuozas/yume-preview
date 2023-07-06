using Realms;
using System;
using System.Linq;

public class StoreManager : IStoreManager
{
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;
    private readonly ICreateStorageHelper _createStorageHelper;

    public StoreManager(IRealmActiveSaveHelper realmActiveSaveHelper, ICreateStorageHelper createStorageHelper)
    {
        _realmActiveSaveHelper = realmActiveSaveHelper;
        _createStorageHelper = createStorageHelper;
    }

    public RealmResult<Store> GetCreateStore(StoreScriptableObject storeScriptable)
    {
        var realm = _realmActiveSaveHelper.GetActiveSave();
        var transaction = realm.StartTransaction();

        if (!realm.TryGet<Store>(storeScriptable.Id, out var store))
            CreateStore(storeScriptable, realm);
        if (!realm.TryGet(storeScriptable.Id, out store))
            throw new ArgumentException("Invalid store, failed to create.");

        transaction.Commit();

        return store;
    }

    private void CreateStore(StoreScriptableObject storeScriptable, Realm realm)
    {
        if(storeScriptable.StoreItems is not { Count: > 0 })
            throw new ArgumentException("Invalid store, no items.");

        var store = new Store
        {
            Id = storeScriptable.Id,
            Name = storeScriptable.StoreName
        };
        realm.Add(store);

        var storage = new Storage
        {
            Name = storeScriptable.StoreName
        };
        _createStorageHelper.CreateStorage(realm, storage);

        var storageSlotsAndPrices = storeScriptable.StoreItems.Select(storeItem =>
        {
            var storageSlot = new StorageSlot
            {
                Item = storeItem.Item,
                Storage = storage
            };
            var storageSlotHasPrice = new StorageSlotHasPrice
            {
                StorageSlot = storageSlot,
                Price = storeItem.Price
            };
            return new
            {
                storageSlot,
                storageSlotHasPrice
            };
        }).ToArray();

        var storageSlots = storageSlotsAndPrices.Select(storageSlotAndPrice => storageSlotAndPrice.storageSlot).ToArray();
        _createStorageHelper.CreateSlots(realm, storageSlots);

        var storageSlotHasPrices = storageSlotsAndPrices.Select(storageSlotAndPrice => storageSlotAndPrice.storageSlotHasPrice).ToArray();
        _createStorageHelper.CreateSlotPrices(realm, storageSlotHasPrices);

        var storeHasStorage = new StoreHasStorage
        {
            Store = store,
            Storage = storage
        };
        realm.Add(storeHasStorage);
    }
}