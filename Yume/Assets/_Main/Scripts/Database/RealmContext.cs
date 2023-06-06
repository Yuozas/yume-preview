using Realms;
using Realms.Schema;

public class RealmContext : IRealmContext
{
    public void DeleteRealm(string name)
    {
        Realm.DeleteRealm(new RealmConfiguration(name));
    }

    public Realm GetGlobalRealm(RealmSchema schema = null)
    {
        var configuration = new RealmConfiguration("Global") 
        {
            Schema = schema
        };
        return Realm.GetInstance(configuration);
    }

    public Realm GetRealm(string name)
    {
        return Realm.GetInstance(new RealmConfiguration(name));
    }
}