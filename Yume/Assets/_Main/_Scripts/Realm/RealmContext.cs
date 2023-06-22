using Realms;

public class RealmContext : IRealmContext
{
    private readonly RealmConfiguration _globalRealmConfiguration = new("Global")
    {
        Schema = new[] { typeof(ActiveRealmSaveDetails), typeof(RealmSaveDetails) }
    };

    public void DeleteRealm(string name)
    {
        var realm = GetRealm(name);
        realm.Write(() =>
        {
            realm.RemoveAll();
        });
    }

    public void DeleteGlobalRealm()
    {
        Realm.DeleteRealm(_globalRealmConfiguration);
    }

    public Realm GetGlobalRealm()
    {
        return Realm.GetInstance(_globalRealmConfiguration);
    }

    public Realm GetRealm(string name)
    {
        return Realm.GetInstance(new RealmConfiguration(name));
    }
}