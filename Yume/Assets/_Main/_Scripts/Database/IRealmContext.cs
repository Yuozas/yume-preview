using Realms;

public interface IRealmContext
{
    public Realm GetRealm(string name);
    public void DeleteRealm(string name);
}