using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class CacheDbContext : ICacheDbContext
{
    private readonly IDataStoreHelper _dataStoreHelper;
    private readonly IMemoryCacheContext _memoryCache;
    private readonly IDbContext _dbContext;
    private readonly Dictionary<Type, bool> _synchronizedTypes;
    private readonly Dictionary<Type, HashSet<int>> _reservedIds;
    private readonly Semaphore _reserveIdSemaphore;

    public CacheDbContext(IMemoryCacheContext memoryCache, IDbContext dbContext,
        IDataStoreHelper dataStoreHelper)
    {
        _dataStoreHelper = dataStoreHelper;
        _memoryCache = memoryCache;
        _dbContext = dbContext;
        _synchronizedTypes = new();
        _reservedIds = new();
        _reserveIdSemaphore = new(1, 1);
    }

    public IEnumerable<T> GetAll<T>() where T : Table, new()
    {
        return _memoryCache.GetAll<T>();
    }

    public IEnumerable<T> GetMultiple<T>(ICollection<int> ids)
    {
        var idsToMatch = ids?.Select(id => id.ToString()).ToArray();
        return _memoryCache.GetMultiple<T>(idsToMatch);
    }
    public IEnumerable<T> GetMultipleMatching<T>(ICollection<int> ids)
    {
        var idsToMatch = ids?.Select(id => id.ToString()).ToArray();
        return _memoryCache.GetMultipleMatching<T>(idsToMatch);
    }

    public T Get<T>(int id) where T : Table, new()
    {
        return _memoryCache.Get<T>(id.ToString());
    }

    public bool TryGet<T>(int id, out T t)
    {
        return _memoryCache.TryGet(id.ToString(), out t);
    }

    public TableQuery<T> GetDbQuery<T>() where T : Table, new()
    {
        // Back up to db if it's unsynchronized for correct data quering.
        if (!TypeIsSynchronized<T>())
            BackupToDb<T>();

        return _dbContext.GetTableQuery<T>();
    }

    public void InsertAll<T>(ICollection<T> instances) where T : Table, new()
    {
        foreach(var instance in instances)
            Insert(instance);
    }

    /// <exception cref="NullReferenceException"></exception>
    public void Insert<T>(T instance) where T : Table, new()
    {
        if (instance is null)
            throw new NullReferenceException("Invalid insert instance. Instance cannot be null.");

        if (instance.Id is 0)
            instance.Id = GetReserveNewId<T>();

        _memoryCache.Set(instance, instance.Id.ToString());

        SetTypeIsUnsynchronized<T>();
    }

    /// <exception cref="ArgumentException">Updating instance with invalid id.</exception>
    public void UpdateAll<T>(ICollection<T> instances) where T : Table, new()
    {
        foreach (var instance in instances)
            Update(instance);
    }

    /// <exception cref="ArgumentException">Updating instance with invalid id.</exception>
    public void Update<T>(T instance) where T : Table, new()
    {
        var key = instance?.Id.ToString();

        if (key is null or "0" || !_memoryCache.Contains<T>(key))
            throw new ArgumentException("Trying to update new instance.");

        _memoryCache.Set(instance, key);

        SetTypeIsUnsynchronized<T>();
    }

    public void BackupToDb<T>() where T : Table, new()
    {
        if (TypeIsSynchronized<T>())
            return;

        var allInstancesOfType = _memoryCache.GetAll<T>();
        foreach(var instance in allInstancesOfType)
        {
            // Get id to unreserve now since it can be modified during insert.
            var idToUnreserve = instance.Id;

            // Backup data.
            _dbContext.InsertOrReplace(instance);

            UnreserveId<T>(idToUnreserve);
        }

        SetTypeIsSynchronized<T>();
    }

    public void BackupAllData()
    {
        this.RunGenericMethodForAllTypes(nameof(BackupToDb), _dataStoreHelper.GetAllTypes().ToArray());
    }

    public void CacheFromDb<T>() where T : Table, new()
    {
        var allInstances = _dbContext.GetTableQuery<T>();
        foreach (var instance in allInstances)
            _memoryCache.Set(instance, instance.Id.ToString());
    }

    public void CacheAllDataFromDb()
    {
        this.RunGenericMethodForAllTypes(nameof(CacheFromDb), _dataStoreHelper.GetAllTypes().ToArray());
    }

    private int GetReserveNewId<T>() where T : Table, new()
    {
        _reserveIdSemaphore.WaitOne();
        try
        {
            var newId = _dbContext.GetNextId<T>();
            var reservedIds = GetSetReservedIds<T>();

            if (reservedIds.Contains(newId))
                newId = reservedIds.Max() + 1;

            reservedIds.Add(newId);

            return newId;
        }
        finally
        {
            _reserveIdSemaphore.Release();
        }
    }

    private void UnreserveId<T>(int id)
    {
        var type = typeof(T);
        if (_reservedIds.TryGetValue(type, out var reservedIds))
            reservedIds.Remove(id);
    }

    private HashSet<int> GetSetReservedIds<T>()
    {
        var type = typeof(T);
        if (_reservedIds.TryGetValue(type, out var reservedIds))
            return reservedIds;

        reservedIds = new();
        _reservedIds.Add(type, reservedIds);
        return reservedIds;
    }

    private void SetTypeIsUnsynchronized<T>()
    {
        _synchronizedTypes[typeof(T)] = false;
    }

    public void SetTypeIsSynchronized<T>()
    {
        _synchronizedTypes[typeof(T)] = true;
    }

    public bool TypeIsSynchronized<T>()
    {
        return _synchronizedTypes.ContainsKey(typeof(T));
    }
}
