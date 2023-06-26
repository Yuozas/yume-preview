using Realms;

public interface IRealmSaveManager
{
    void CreateNewSave(CharacterRealmObject character);
    void DeleteSave(long saveId);
    void CopySave(long saveId);
}