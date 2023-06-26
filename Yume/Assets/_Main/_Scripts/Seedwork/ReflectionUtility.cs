using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class ReflectionUtility
{
    public static Dictionary<long, T> GetStaticFieldDictionaryWithId<T>() where T : struct
    {
        return typeof(T)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(field => field.FieldType == typeof(T))
            .ToDictionary(
                field => (long)field.FieldType.GetProperty("Id").GetValue(field.GetValue(null)),
                field => (T)field.GetValue(null)
            );
    }
}