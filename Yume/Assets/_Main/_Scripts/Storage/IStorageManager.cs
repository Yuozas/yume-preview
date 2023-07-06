using Realms;

public interface IStorageManager
{
    RealmResult<Storage> GetCreateStorage(StorageScriptableObject storageScriptableObject);
}