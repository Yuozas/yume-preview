using System.Collections.Generic;

public interface IMemoryCacheContext
{
    bool Contains<T>(string key);
    IEnumerable<T> GetAll<T>();
    IEnumerable<T> GetMultipleMatching<T>(ICollection<string> keys);

    /// <returns>Not null instances found by keys.</returns>
    /// <exception cref="NullReferenceException">Not found instance.</exception>
    IEnumerable<T> GetMultiple<T>(ICollection<string> keys);

    /// <returns>Not null instance found by key.</returns>
    /// <exception cref="NullReferenceException">Not found instance.</exception>
    T Get<T>(string key);

    bool TryGet<T>(string key, out T t);
    void Set<T>(T instance, string key);
}