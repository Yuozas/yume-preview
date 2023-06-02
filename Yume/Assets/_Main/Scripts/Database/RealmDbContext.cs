using Realms;
using System;
using System.Linq;
using System.Linq.Expressions;

public class RealmDbContext : ITransientDbContext
{
    private readonly Realm _realm;
    private Transaction _transaction;

    public RealmDbContext()
    {
        _realm = Realm.GetInstance();
    }

    public IQueryable<T> All<T>() where T : class
    {
        AssertRealmObject<T>();

        var allMethod = typeof(Realm).GetMethod("All").MakeGenericMethod(typeof(T));
        IQueryable<T> realmObjects = (IQueryable<T>)allMethod.Invoke(_realm, null);

        return realmObjects;
    }

    public void Add<T>(T entity) where T : class
    {
        AssertRealmObject<T>();
        _realm.Add((RealmObject)(object)entity);
    }

    public void Update<T>(T entity) where T : class
    {
        AssertRealmObject<T>();
        _realm.Add((RealmObject)(object)entity, update: true);
    }

    public void Delete<T>(T entity) where T : class
    {
        AssertRealmObject<T>();
        _realm.Remove((RealmObject)(object)entity);
    }

    public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
    {
        AssertRealmObject<T>();
        return All<T>().Where(predicate);
    }

    public void BeginTransaction()
    {
        _transaction = _realm.BeginWrite();
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
        _transaction.Dispose();
    }

    public void CancelTransaction()
    {
        _transaction.Rollback();
        _transaction.Dispose();
    }

    private void AssertRealmObject<T>() where T : class
    {
        if (!typeof(RealmObject).IsAssignableFrom(typeof(T)))
            throw new ArgumentException($"Type {typeof(T).FullName} is not a RealmObject.");
    }

    public void Dispose()
    {
        _realm.Dispose();
    }
}