using MongoDB.Bson;
using System;

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
        var newSave = new RealmSaveDetails
        {
            SaveId = ObjectId.GenerateNewId().ToString(),
            DisplayName = saveName,
            Date = DateTime.UtcNow,
            IsVisible = true
        };
        globalRealm.WriteAdd(newSave);
        ChangeActiveSave(newSave);
    }

    public void DeleteSave(string saveId)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        globalRealm.TryWriteUpdate<RealmSaveDetails>(saveId, realmSaveDetails =>
        {
            realmSaveDetails.IsVisible = false;
        });
    }

    public void CopySave(string saveId)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();

        if(!globalRealm.TryGet<RealmSaveDetails>(saveId, out var save))
            throw new ArgumentException($"Invalid {nameof(saveId)} passed. Save doesn't exist.");

        var newSave = new RealmSaveDetails
        {
            SaveId = ObjectId.GenerateNewId().ToString(),
            DisplayName = save.DisplayName,
            Date = DateTime.UtcNow
        };
        globalRealm.WriteAdd(newSave);
        ChangeActiveSave(newSave);
    }

    public void ChangeActiveSave(RealmSaveDetails realmSave)
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        globalRealm.WriteUpsert<ActiveRealmSaveDetails>(realmSaveDetails =>
        {
            realmSaveDetails.ActiveSaveDetails = realmSave;
        });
    }

    public RealmResult<ActiveRealmSaveDetails> GetActiveSaveDetails()
    {
        var globalRealm = _realmContext.GetGlobalRealm();
        if(globalRealm.TryGet<ActiveRealmSaveDetails>(out var activeRealmSave) && activeRealmSave.ActiveSaveDetails.IsVisible)
            return activeRealmSave;
        return null;
    }
}
