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
        var activeRealmSave = globalRealm.All<ActiveRealmSave>().FirstOrDefault();
        if (activeRealmSave is not null)
            globalRealm.Remove(activeRealmSave);
        var newSave = new RealmSave
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
        var save = globalRealm.Find<RealmSave>(saveId);
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

        var save = globalRealm.Find<RealmSave>(saveId) 
            ?? throw new ArgumentException($"Invalid {nameof(saveId)} passed. Save doesn't exist.");

        var newSave = new RealmSave
        {
            SaveId = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            DisplayName = save.DisplayName,
            Date = DateTime.UtcNow
        };
        globalRealm.Add(newSave);
        ChangeActiveSave(newSave);
        transaction.Commit();
    }

    public void ChangeActiveSave(RealmSave realmSave)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        var activeRealmSave = globalRealm.All<ActiveRealmSave>().FirstOrDefault();
        if (activeRealmSave is null)
        {
            activeRealmSave = new ActiveRealmSave { ActiveSave = realmSave };
            globalRealm.Add(activeRealmSave);
            return;
        }
        activeRealmSave.ActiveSave = realmSave;
        globalRealm.Add(realmSave, true);
    }

    public ActiveRealmSave GetActiveSave()
    {
        using var globalRealm = _realmContext.GetGlobalRealm(new[] { typeof(ActiveRealmSave), typeof(RealmSave) });

        var activeRealmSave = globalRealm.All<ActiveRealmSave>().FirstOrDefault();
        if(activeRealmSave?.ActiveSave?.IsVisible ?? false)
            return activeRealmSave;

        return null;
    }
}
