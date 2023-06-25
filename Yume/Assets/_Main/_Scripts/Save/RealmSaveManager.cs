using FastDeepCloner;
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

        using var realm = GetActiveSave();
        realm.WriteAdd(new PlayerDetails()
        {
            CharacterId = character.Id,
            SceneName = character.SceneName
        });
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
        using var realm = _realmContext.GetGlobalRealm();
        return realm.All<RealmSaveDetails>().Any(s => s.IsVisible);
    }

    /// <exception cref="ArgumentException">No active save found.</exception>
    public Realm GetActiveSave()
    {
        using var activeSaveDetails = GetActiveSaveDetails();
        return activeSaveDetails is null
            ? throw new ArgumentException("No active save found.")
            : _realmContext.GetRealm(activeSaveDetails.Result.SaveId.ToString());
    }

    public void ChangeActiveSave(long saveId)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        var save = globalRealm.Find<RealmSaveDetails>(saveId);
        ChangeActiveSave(save);
    }

    public void ChangeActiveSave(RealmSaveDetails realmSave)
    {
        _realmSaveRegistry.ChangeActiveSave(realmSave);
    }

    public RealmSaveDetails[] GetAllSaveDetails()
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        return globalRealm.All<RealmSaveDetails>().ToArray().Select(save => save.Clone()).ToArray();
    }

    public RealmResult<RealmSaveDetails> GetActiveSaveDetails()
    {
        return _realmSaveRegistry.GetActiveSaveDetails()?.Result.ActiveSaveDetails;
    }
}
