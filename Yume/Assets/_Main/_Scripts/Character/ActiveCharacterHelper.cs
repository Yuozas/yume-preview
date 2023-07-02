using System.Linq;

public class ActiveCharacterHelper : IActiveCharacterHelper
{
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;

    public ActiveCharacterHelper(IRealmActiveSaveHelper realmActiveSaveHelper)
    {
        _realmActiveSaveHelper = realmActiveSaveHelper;
    }

    public RealmResult<Storage> GetBackpack()
    {
        var activeRealm = _realmActiveSaveHelper.GetActiveSave();
        return activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage;
    }
}