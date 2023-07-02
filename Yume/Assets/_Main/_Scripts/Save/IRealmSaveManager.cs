public interface IRealmSaveManager
{
    void CreateNewSave(CharacterRealmObject character);
    void DeleteSave(string saveId);
    void CopySave(string saveId);
}