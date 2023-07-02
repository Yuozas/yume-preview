using Realms;

public interface IRealmActiveSaveHelper
{
    /// <exception cref="ArgumentException">No active save found.</exception>
    Realm GetActiveSave();

    RealmResult<RealmSaveDetails> GetActiveSaveDetails();
    void ChangeActiveSave(string saveId);
    void ChangeActiveSave(RealmSaveDetails realmSave);
}