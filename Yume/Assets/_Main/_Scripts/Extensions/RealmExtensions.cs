using Realms;
using System;
using System.Linq;

public static class RealmExtensions
{
    public static T Get<T>(this Realm realm) where T : RealmObject
    {
        return realm.All<T>().First();
    }

    public static bool TryGet<T>(this Realm realm, long key, out T obj) where T : RealmObject
    {
        obj = realm.Find<T>(key);
        return obj is not null;
    }

    public static bool TryGet<T>(this Realm realm, out T obj) where T : RealmObject
    {
        obj = realm.All<T>().FirstOrDefault();
        return obj is not null;
    }

    public static void WriteUpdate<T>(this Realm realm, long key, Action<T> update) where T : RealmObject
    {
        if(realm.TryGet<T>(key, out var t))
            realm.WriteUpsert(() =>
            {
                update(t);
                return t;
            });
    }

    public static void WriteUpdate<T>(this Realm realm, Action<T> update) where T : RealmObject
    {
        if(realm.TryGet<T>(out var t))
            realm.WriteUpsert(() =>
            {
                update(t);
                return t;
            });
    }

    public static void WriteUpsert<T>(this Realm realm, long key, Action<T> update) where T : RealmObject, new()
    {
        if (realm.TryGet<T>(key, out var t))
        {
            realm.WriteUpdate(update);
            return;
        }
        var @object = new T();
        update(@object);
        realm.WriteAdd(@object);
    }

    public static void WriteUpsert<T>(this Realm realm, Action<T> update) where T : RealmObject, new()
    {
        if (realm.TryGet<T>(out var t))
        {
            realm.WriteUpdate(update);
            return;
        }
        var @object = new T();
        update(@object);
        realm.WriteAdd(@object);
    }

    public static void WriteUpsert<T>(this Realm realm, Func<T> func) where T : RealmObject
    {
        realm.WriteSafe(() => realm.Add(func(), true));
    }

    public static void WriteUpsert<T>(this Realm realm, T obj) where T : RealmObject
    {
        realm.WriteSafe(() => realm.Add(obj, true));
    }

    public static void WriteAdd<T>(this Realm realm, T obj) where T : RealmObject
    {
        realm.WriteSafe(() => realm.Add(obj));
    }

    public static void WriteSafe(this Realm realm, Action action)
    {
        var hasOwnTransaction = realm.IsInTransaction;

        Transaction newTransaction = null;
        if (!hasOwnTransaction)
            newTransaction = realm.BeginWrite();

        action();

        if (!hasOwnTransaction)
            newTransaction.Commit();
    }
}