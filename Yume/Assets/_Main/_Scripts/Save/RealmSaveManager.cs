public class RealmSaveManager : IRealmSaveManager
{
    private readonly RealmSaveRegistry _realmSaveRegistry;
    private readonly ICreateStorageHelper _createStorageHelper;
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;

    public RealmSaveManager(RealmSaveRegistry realmSaveRegistry, ICreateStorageHelper createStorageHelper, 
        IRealmActiveSaveHelper realmActiveSaveHelper)
    {
        _realmSaveRegistry = realmSaveRegistry;
        _createStorageHelper = createStorageHelper;
        _realmActiveSaveHelper = realmActiveSaveHelper;
    }

    public void CreateNewSave(CharacterRealmObject character)
    {
        _realmSaveRegistry.CreateNewSave($"{character.Name}'s little story.");

        using var realm = _realmActiveSaveHelper.GetActiveSave();
        using var transaction = realm.BeginWrite();
        realm.Add(character);
        realm.Add(new ActiveCharacer { Character = character });
        _createStorageHelper.CreateStorageForCharacter(realm, StorageScriptableObject.DemoBackpack);

        transaction.Commit();
    }

    public void DeleteSave(string saveId)
    {
        _realmSaveRegistry.DeleteSave(saveId);
    }

    public void CopySave(string saveId)
    {
        _realmSaveRegistry.CopySave(saveId);
    }
}