using System;

public class StoreReadHelper : IStoreReadHelper
{
    private readonly IRealmActiveSaveHelper _activeSaveHelper;

    public StoreReadHelper(IRealmActiveSaveHelper activeSaveHelper)
    {
        _activeSaveHelper = activeSaveHelper;
    }

    public RealmResult<Store> GetStore(string storeId) 
    {
        var realm = _activeSaveHelper.GetActiveSave();

        if (!realm.TryGet<Store>(storeId, out var store))
            throw new ArgumentException($"Invalid {storeId}");

        return store;
    }
}