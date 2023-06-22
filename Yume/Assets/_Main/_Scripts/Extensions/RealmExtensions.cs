using Realms;
using System.Linq;

public static class RealmExtensions
{
    public static T Get<T>(this Realm realm) where T : RealmObject
    {
        return realm.All<T>().First();
    }

    public static void UpdateAndSave(this Realm realm, RealmObject obj)
    {
        realm.Write(() => realm.Add(obj, true));
    }

    public static void AddAndSave(this Realm realm, RealmObject obj)
    {
        realm.Write(() => realm.Add(obj));
    }
}