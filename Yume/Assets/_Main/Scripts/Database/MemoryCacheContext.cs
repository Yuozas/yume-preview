using System;
using System.Collections.Generic;
using System.Linq;
using FastDeepCloner;

public class MemoryCacheContext : IMemoryCacheContext
{
    private readonly Dictionary<Type, Dictionary<string, object>> _typeCache;

    public MemoryCacheContext()
    {
        _typeCache = new Dictionary<Type, Dictionary<string, object>>();
    }

    public bool Contains<T>(string key)
    {
        var cache = GetOrAddCache<T>();
        return cache.ContainsKey(key);
    }

    public IEnumerable<T> GetAll<T>()
    {
        var cache = GetOrAddCache<T>();
        foreach(var cacheEntry in cache)
            yield return (T)cacheEntry.Value;
    }

    public IEnumerable<T> GetMultipleMatching<T>(ICollection<string> keys)
    {
        if (keys is null or { Count: < 1 })
            yield break;

        var uniqueKeys = keys.ToHashSet();
        foreach (var key in uniqueKeys)
            if (TryGet<T>(key, out var t))
                yield return t;
    }

    public IEnumerable<T> GetMultiple<T>(ICollection<string> keys)
    {
        if (keys is null or { Count: < 1 })
            yield break;

        var uniqueKeys = keys.ToHashSet();
        foreach (var key in uniqueKeys)
            yield return Get<T>(key);
    }

    public T Get<T>(string key)
    {
        if(TryGet(key, out T t))
            return t;
        throw new NullReferenceException("Invalid key. Object not found.");
    }

    public bool TryGet<T>(string key, out T t)
    {
        var cache = GetOrAddCache<T>();
        if(cache.TryGetValue(key, out var cachedT))
        {
            t = (T)DeepCloner.Clone(cachedT);
            return true;
        }
        t = default;
        return false;
    }

    public void Set<T>(T instance, string key)
    {
        var cache = GetOrAddCache<T>();
        cache[key] = instance;
    }

    private Dictionary<string, object> GetOrAddCache<T>()
    {
        var type = typeof(T);
        if(_typeCache.TryGetValue(type, out var cache))
            return cache;
        cache = new();
        _typeCache.Add(type, cache);
        return cache;
    }
}
