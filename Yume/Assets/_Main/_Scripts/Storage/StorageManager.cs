using System;

public class StorageManager : IStorageManager
{
    private readonly ICreateStorageHelper _createStorageHelper;
    private readonly IRealmActiveSaveHelper _activeSaveHelper;

    public StorageManager(ICreateStorageHelper createStorageHelper, 
        IRealmActiveSaveHelper activeSaveHelper)
    {
        _createStorageHelper = createStorageHelper;
        _activeSaveHelper = activeSaveHelper;
    }

    public RealmResult<Storage> GetCreateStorage(StorageScriptableObject storageScriptableObject)
    {
        if(storageScriptableObject == null)
            throw new ArgumentNullException(nameof(storageScriptableObject));

        var realm = _activeSaveHelper.GetActiveSave();

        if (realm.TryGet<Storage>(storageScriptableObject.Id, out var storage))
            return storage;

        var transaction = realm.StartTransaction();
        _createStorageHelper.CreateStorage(realm, storageScriptableObject);
        transaction.Commit();

        if (realm.TryGet(storageScriptableObject.Id, out storage))
            return storage;

        throw new Exception("Failed to create storage");
    }
}