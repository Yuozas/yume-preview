using Realms;

public class RealmContext : IRealmContext
{
    public void DeleteRealm(string name)
    {
        Realm.DeleteRealm(new RealmConfiguration(name));
    }

    public Realm GetRealm(string name)
    {
        return Realm.GetInstance(new RealmConfiguration(name));
    }
}