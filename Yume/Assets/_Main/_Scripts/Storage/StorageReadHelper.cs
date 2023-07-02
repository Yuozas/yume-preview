using Realms;
using System;

public class StorageReadHelper : IStorageReadHelper
{
    private readonly IRealmActiveSaveHelper _activeSaveHelper;

    public StorageReadHelper(IRealmActiveSaveHelper activeSaveHelper)
    {
        _activeSaveHelper = activeSaveHelper;
    }

    public RealmResult<Storage> GetStorage(string storageId)
    {
        var realm = _activeSaveHelper.GetActiveSave();

        if (!realm.TryGet<Storage>(storageId, out var storage))
            throw new ArgumentException($"Invalid {storageId}");

        return storage;
    }

    public RealmResult<StorageSlot> GetStorageSlot(string storageSlotId)
    {
        var realm = _activeSaveHelper.GetActiveSave();

        if (!realm.TryGet<StorageSlot>(storageSlotId, out var storageSlot))
            throw new ArgumentException($"Invalid {storageSlotId}");

        return storageSlot;
    }
}