using FastDeepCloner;
using System.Linq;

public class RealmSaveReadHelper : IRealmSaveReadHelper
{
    private readonly IRealmContext _realmContext;

    public RealmSaveReadHelper(IRealmContext realmContext)
    {
        _realmContext = realmContext;
    }

    public bool AnySaveExists()
    {
        using var realm = _realmContext.GetGlobalRealm();
        return realm.All<RealmSaveDetails>().Any(s => s.IsVisible);
    }

    public RealmSaveDetails[] GetAllSaveDetails()
    {
        using var globalRealm = _realmContext.GetGlobalRealm();
        return globalRealm.All<RealmSaveDetails>().ToArray().Select(save => save.Clone()).ToArray();
    }
}