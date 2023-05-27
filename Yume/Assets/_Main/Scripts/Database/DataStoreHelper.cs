using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class DataStoreHelper : IDataStoreHelper
{
    public IEnumerable<Type> GetAllTypes()
    {
        var abstractType = typeof(Table);
        var assembly = Assembly.GetExecutingAssembly();

        return assembly
            .GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                abstractType.IsAssignableFrom(type) &&
                type != abstractType
            );
    }

    public IEnumerable<string> GetAllTypeNames()
    {
        return GetAllTypes().Select(t => t.Name);
    }
}
