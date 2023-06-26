using Realms;

public interface IRealmSaveManager
{
    void CreateNewSave(CharacterRealmObject character);
    void DeleteSave(long saveId);
    void CopySave(long saveId);
    bool AnySaveExists();

    /// <exception cref="ArgumentException">No active save found.</exception>
    Realm GetActiveSave();
    
    void ChangeActiveSave(RealmSaveDetails realmSave);
    void ChangeActiveSave(long saveId);
    RealmSaveDetails[] GetAllSaveDetails();
    RealmResult<RealmSaveDetails> GetActiveSaveDetails();
}