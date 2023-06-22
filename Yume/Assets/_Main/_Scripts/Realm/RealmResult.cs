using FastDeepCloner;
using Realms;
using System;

public class RealmResult<T> : IDisposable
    where T : RealmObject
{
    public RealmResult(T result)
    {
        Realm = result.Realm;
        Result = result;
    }

    public Realm Realm { get; }
    public T Result { get; }

    public T Clone()
    {
        return Result.Clone();
    }

    public void Dispose()
    {
        Realm.Dispose();
    }

    public static implicit operator RealmResult<T>(T realmObject)
    {
        if(realmObject is null)
            return null;
        return new(realmObject);
    }
}
