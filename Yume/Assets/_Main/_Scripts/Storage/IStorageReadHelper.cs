using Realms;

public interface IStorageReadHelper
{
    RealmResult<Storage> GetStorage(string storageId);
    RealmResult<StorageSlot> GetStorageSlot(string storageSlotId);
}