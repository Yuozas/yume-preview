using FastDeepCloner;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

public static class RealmExtensions
{
    public static T[] GetDetatched<T>(this IQueryable<T> values) where T : RealmObject
    {
        var result = new List<T>();
        foreach(var value in values)
            result.Add(value.Clone(FieldType.PropertyInfo));
        return result.ToArray();
    }

    public static T Get<T>(this Realm realm) 
        where T : RealmObject
    {
        return realm.All<T>().First();
    }

    public static bool TryGet<T>(this Realm realm, string key, out T realmObject) 
        where T : RealmObject
    {
        realmObject = realm.Find<T>(key);
        return realmObject is not null;
    }

    public static bool TryGet<T>(this Realm realm, out T realmObject) 
        where T : RealmObject
    {
        realmObject = realm.All<T>().FirstOrDefault();
        return realmObject is not null;
    }

    public static void WriteUpsert<T>(this Realm realm, string key, Action<T> update)
    where T : RealmObject, new()
    {
        if (!realm.TryWriteUpdate(key, update))
            realm.WriteAdd(update);
    }

    public static void WriteUpsert<T>(this Realm realm, Action<T> update)
        where T : RealmObject, new()
    {
        if (!realm.TryWriteUpdate(update))
            realm.WriteAdd(update);
    }

    public static void WriteUpsert<T>(this Realm realm, Func<T> write)
        where T : RealmObject
    {
        realm.WriteSafe(() => realm.Add(write(), true));
    }

    public static void WriteUpsert<T>(this Realm realm, T realmObject)
        where T : RealmObject
    {
        realm.WriteSafe(() => realm.Add(realmObject, true));
    }

    public static bool TryWriteUpdate<T>(this Realm realm, string key, Action<T> update) 
        where T : RealmObject
    {
        if (!realm.TryGet<T>(key, out var realmObject))
            return false;
        realm.WriteUpsert(() =>
        {
            update(realmObject);
            return realmObject;
        });
        return true;
    }

    public static bool TryWriteUpdate<T>(this Realm realm, Action<T> update) 
        where T : RealmObject
    {
        if (!realm.TryGet<T>(out var realmObject))
            return false;
        realm.WriteUpsert(() =>
        {
            update(realmObject);
            return realmObject;
        });
        return true;
    }

    public static void WriteAdd<T>(this Realm realm, Action<T> update) 
        where T : RealmObject, new()
    {
        var @object = new T();
        update(@object);
        realm.WriteAdd(@object);
    }

    public static void WriteAdd<T>(this Realm realm, T realmObject) 
        where T : RealmObject
    {
        realm.WriteSafe(() => realm.Add(realmObject));
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

    public static Transaction StartTransaction(this Realm realm)
    {
        return realm.BeginWrite();
    }
}