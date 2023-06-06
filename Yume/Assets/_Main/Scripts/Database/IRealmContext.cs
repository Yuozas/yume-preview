using Realms;
using Realms.Schema;

public interface IRealmContext
{
    Realm GetRealm(string name);
    void DeleteRealm(string name);
    Realm GetGlobalRealm(RealmSchema schema = null);
}