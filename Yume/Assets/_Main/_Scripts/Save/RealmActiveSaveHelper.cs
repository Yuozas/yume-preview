using Realms;
using System;

public class RealmActiveSaveHelper : IRealmActiveSaveHelper
{
    private readonly IRealmContext _realmContext;
    private readonly RealmSaveRegistry _realmSaveRegistry;

    public RealmActiveSaveHelper(IRealmContext realmContext, RealmSaveRegistry realmSaveRegistry)
    {
        _realmContext = realmContext;
        _realmSaveRegistry = realmSaveRegistry;
    }

    /// <exception cref="ArgumentException">No active save found.</exception>
    public Realm GetActiveSave()
    {
        using var activeSaveDetails = GetActiveSaveDetails();
        return activeSaveDetails is null
            ? throw new ArgumentException("No active save found.")
            : _realmContext.GetRealm(activeSaveDetails.Result.SaveId.ToString());
    }

    public RealmResult<RealmSaveDetails> GetActiveSaveDetails()
    {
        return _realmSaveRegistry.GetActiveSaveDetails()?.Result.ActiveSaveDetails;
    }

    public void ChangeActiveSave(string saveId)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        var save = globalRealm.Find<RealmSaveDetails>(saveId);
        ChangeActiveSave(save);
    }

    public void ChangeActiveSave(RealmSaveDetails realmSave)
    {
        _realmSaveRegistry.ChangeActiveSave(realmSave);
    }
}