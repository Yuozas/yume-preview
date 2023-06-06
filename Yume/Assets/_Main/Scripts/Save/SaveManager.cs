using Realms;
using System;
using System.Linq;

public class SaveManager : ISaveManager
{
    private readonly IRealmContext _realmContext;
    private readonly RealmSaveRegistry _realmSaveRegistry;

    public SaveManager(IRealmContext realmContext, RealmSaveRegistry realmSaveRegistry)
    {
        _realmContext = realmContext;
        _realmSaveRegistry = realmSaveRegistry;
    }

    public void CreateNewSave(string saveName = "New save")
    {
        _realmSaveRegistry.CreateNewSave(saveName);
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
        var activeSave = _realmSaveRegistry.GetActiveSave();
        if(activeSave is null)
            return false;
        var save = globalRealm.Find<RealmSave>(activeSave.ActiveSave.SaveId);
        return save is not null;
    }

    public Realm GetActiveSave()
    {
        var activeSave = _realmSaveRegistry.GetActiveSave();
        return activeSave is null
            ? throw new ArgumentException("No active save found.")
            : _realmContext.GetRealm(activeSave.ActiveSave.SaveId.ToString());
    }

    public void ChangeActiveSave(RealmSave realmSave)
    {
        _realmSaveRegistry.ChangeActiveSave(realmSave);
    }

    public RealmSave[] GetSaves()
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        return globalRealm.All<RealmSave>().ToArray();
    }
}
