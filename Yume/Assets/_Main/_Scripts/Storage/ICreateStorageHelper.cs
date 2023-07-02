using Realms;

public interface ICreateStorageHelper
{
    void CreateStorageForCharacter(Realm realm, StorageScriptableObject storage);
    void CreateStorage(Realm realm, StorageScriptableObject storage);
}