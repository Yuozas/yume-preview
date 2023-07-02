using UnityEngine;

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
        _createStorageHelper.CreateStorageForCharacter(realm, GetDemoStorage());

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

    // Todo. Refactor. This should not be here.
    private StorageScriptableObject GetDemoStorage()
    {
        var demoBackpack = ScriptableObject.CreateInstance<StorageScriptableObject>();
        demoBackpack.Id = "1";
        demoBackpack.Name = "Demo Backpack";
        demoBackpack.Slots = 10;
        return demoBackpack;
    }
}