using Realms;
using System.Linq;

public static class RealmExtensions
{
    public static T Get<T>(this Realm realm) where T : RealmObject
    {
        return realm.All<T>().First();
    }

    public static bool TryGet<T>(this Realm realm, out T obj) where T : RealmObject
    {
        obj = realm.All<T>().FirstOrDefault();
        return obj != null;
    }

    public static void WriteUpsert<T>(this Realm realm, T obj) where T : RealmObject
    {
        realm.Write(() => realm.Add(obj, true));
    }

    public static void WriteAdd<T>(this Realm realm, T obj) where T : RealmObject
    {
        realm.Write(() => realm.Add(obj));
    }
}