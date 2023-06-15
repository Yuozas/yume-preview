using Realms;
using System;
using System.Linq;

public class RealmSaveManager : IRealmSaveManager
{
    private readonly IRealmContext _realmContext;
    private readonly RealmSaveRegistry _realmSaveRegistry;
    private readonly CharacterDataHandler _characterDataHandler;

    public RealmSaveManager(IRealmContext realmContext, RealmSaveRegistry realmSaveRegistry, 
        CharacterDataHandler characterDataHandler)
    {
        _realmContext = realmContext;
        _realmSaveRegistry = realmSaveRegistry;
        _characterDataHandler = characterDataHandler;
    }

    public void CreateNewSave(int characterId)
    {
        var character = _characterDataHandler.GetCharacterData(characterId);
        if(character.Type is not DefaultCharacterData.MAIN_TYPE)
            throw new ArgumentException("Only main characters can be used to create a new save");
        _realmSaveRegistry.CreateNewSave($"{character.Name}'s little story.");

        var realm = GetActiveSave();
        var activeCharacter = realm.Get<ActiveCharacter>();
        activeCharacter.Id = character.Id;
        realm.Update(activeCharacter);
    }

    public void DeleteSave(long saveId)
    {
        _realmSaveRegistry.DeleteSave(saveId);
    }

    public void CopySave(long saveId)
    {
        _realmSaveRegistry.CopySave(saveId);
    }

    public bool AnySaveExists()
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        var activeSave = _realmSaveRegistry.GetActiveSaveDetails();
        if(activeSave is null)
            return false;
        var save = globalRealm.Find<RealmSaveDetails>(activeSave.ActiveSaveDetails.SaveId);
        return save is not null;
    }

    public Realm GetActiveSave()
    {
        var activeSave = _realmSaveRegistry.GetActiveSaveDetails();
        return activeSave is null
            ? throw new ArgumentException("No active save found.")
            : _realmContext.GetRealm(activeSave.ActiveSaveDetails.SaveId.ToString());
    }

    public void ChangeActiveSave(RealmSaveDetails realmSave)
    {
        _realmSaveRegistry.ChangeActiveSave(realmSave);
    }

    public RealmSaveDetails[] GetAllSaveDetails()
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        return globalRealm.All<RealmSaveDetails>().ToArray();
    }

    public RealmSaveDetails GetActiveSaveDetails()
    {
        return _realmSaveRegistry.GetActiveSaveDetails().ActiveSaveDetails;
    }
}
