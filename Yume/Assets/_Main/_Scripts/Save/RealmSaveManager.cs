using FastDeepCloner;
using Realms;
using System;
using System.Linq;

public class RealmSaveManager : IRealmSaveManager
{
    private readonly IRealmContext _realmContext;
    private readonly RealmSaveRegistry _realmSaveRegistry;

    public RealmSaveManager(IRealmContext realmContext, RealmSaveRegistry realmSaveRegistry)
    {
        _realmContext = realmContext;
        _realmSaveRegistry = realmSaveRegistry;
    }

    public void CreateNewSave(CharacterRealmObject character)
    {
        _realmSaveRegistry.CreateNewSave($"{character.Name}'s little story.");

        using var realm = GetActiveSave();
        using var transaction = realm.BeginWrite();
        realm.Add(character);
        realm.Add(new ActiveCharacer { Character = character });
        transaction.Commit();
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
