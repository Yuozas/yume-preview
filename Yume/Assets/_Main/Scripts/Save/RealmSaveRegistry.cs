using System;
using System.Linq;

public class RealmSaveRegistry
{
    private readonly IRealmContext _realmContext;

    public RealmSaveRegistry(IRealmContext realmContext)
    {
        _realmContext = realmContext;
    }

    public void CreateNewSave(string saveName = "New save")
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        using var transaction = globalRealm.BeginWrite();
        var activeRealmSave = globalRealm.All<ActiveRealmSaveDetails>().FirstOrDefault();
        if (activeRealmSave is not null)
            globalRealm.Remove(activeRealmSave);
        var newSave = new RealmSaveDetails
        {
            SaveId = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            DisplayName = saveName,
            Date = DateTime.UtcNow
        };
        globalRealm.Add(newSave);
        ChangeActiveSave(newSave);
        transaction.Commit();
    }

    public void DeleteSave(long saveId)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        using var transaction = globalRealm.BeginWrite();
        var save = globalRealm.Find<RealmSaveDetails>(saveId);
        if (save is not null)
        {
            save.IsVisible = false; 
            globalRealm.Add(save, true);
        }
            
        transaction.Commit();
    }

    public void CopySave(long saveId)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        using var transaction = globalRealm.BeginWrite();

        var save = globalRealm.Find<RealmSaveDetails>(saveId) 
            ?? throw new ArgumentException($"Invalid {nameof(saveId)} passed. Save doesn't exist.");

        var newSave = new RealmSaveDetails
        {
            SaveId = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            DisplayName = save.DisplayName,
            Date = DateTime.UtcNow
        };
        globalRealm.Add(newSave);
        ChangeActiveSave(newSave);
        transaction.Commit();
    }

    public void ChangeActiveSave(RealmSaveDetails realmSave)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        var activeRealmSave = globalRealm.All<ActiveRealmSaveDetails>().FirstOrDefault();
        if (activeRealmSave is null)
        {
            activeRealmSave = new ActiveRealmSaveDetails { ActiveSaveDetails = realmSave };
            globalRealm.Add(activeRealmSave);
            return;
        }
        activeRealmSave.ActiveSaveDetails = realmSave;
        globalRealm.Add(realmSave, true);
    }

    public ActiveRealmSaveDetails GetActiveSaveDetails()
    {
        using var globalRealm = _realmContext.GetGlobalRealm();

        var activeRealmSave = globalRealm.All<ActiveRealmSaveDetails>().FirstOrDefault();
        if(activeRealmSave?.ActiveSaveDetails?.IsVisible ?? false)
            return activeRealmSave;

        return null;
    }
}
