using Realms;
public interface IRealmContext
{
    Realm GetRealm(string name);
    void DeleteRealm(string name);
    Realm GetGlobalRealm();
    void DeleteGlobalRealm();
}