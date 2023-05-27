using SQLite4Unity3d;
using System.Collections.Generic;

public interface ICacheDbContext
{
    IEnumerable<T> GetAll<T>() where T : Table, new();

    /// <returns>Not null instances found by keys.</returns>
    /// <exception cref="NullReferenceException">Not found instance.</exception>
    IEnumerable<T> GetMultiple<T>(ICollection<int> ids);

    IEnumerable<T> GetMultipleMatching<T>(ICollection<int> ids);

    /// <returns>Not null instance found by key.</returns>
    /// <exception cref="NullReferenceException">Not found instance.</exception>
    T Get<T>(int id) where T : Table, new();

    bool TryGet<T>(int id, out T t);
    TableQuery<T> GetDbQuery<T>() where T : Table, new();

    /// <exception cref="NullReferenceException"></exception>
    void InsertAll<T>(ICollection<T> instances) where T : Table, new();

    /// <exception cref="NullReferenceException"></exception>
    void Insert<T>(T instance) where T : Table, new();

    /// <exception cref="ArgumentException">Updating instance with invalid id.</exception>
    void UpdateAll<T>(ICollection<T> instances) where T : Table, new();

    /// <exception cref="ArgumentException">Updating instance with invalid id.</exception>
    void Update<T>(T instance) where T : Table, new();

    void BackupToDb<T>() where T : Table, new();
    void BackupAllData();
    void CacheFromDb<T>() where T : Table, new();
    void CacheAllDataFromDb();
}