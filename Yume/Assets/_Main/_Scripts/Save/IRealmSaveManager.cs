using Realms;

public interface IRealmSaveManager
{
    void CreateNewSave(int characterId);
    void DeleteSave(long saveId);
    void CopySave(long saveId);
    bool AnySaveExists();
    Realm GetActiveSave();
    void ChangeActiveSave(RealmSaveDetails realmSave);
    void ChangeActiveSave(long saveId);
    RealmSaveDetails[] GetAllSaveDetails();
    RealmResult<RealmSaveDetails> GetActiveSaveDetails();
}