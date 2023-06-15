using Realms;
using Realms.Schema;

public class RealmContext : IRealmContext
{
    public void DeleteRealm(string name)
    {
        Realm.DeleteRealm(new RealmConfiguration(name));
    }

    public Realm GetGlobalRealm()
    {
        var configuration = new RealmConfiguration("Global") 
        {
            // Hardcode all unset schema types here to avoid runtime errors.
            // This must always be the same.
            Schema = new []{ typeof(ActiveRealmSaveDetails), typeof(RealmSaveDetails) }
        };
        return Realm.GetInstance(configuration);
    }

    public Realm GetRealm(string name)
    {
        return Realm.GetInstance(new RealmConfiguration(name));
    }
}