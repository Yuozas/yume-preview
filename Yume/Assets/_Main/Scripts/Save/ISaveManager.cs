using Realms;

public interface ISaveManager
{
    void CreateNewSave(string saveName = "New save");
    void DeleteSave(long saveId);
    void CopySave(long saveId);
    bool AnySaveExists();
    Realm GetActiveSave();
    void ChangeActiveSave(RealmSave realmSave);
    RealmSave[] GetSaves();
}