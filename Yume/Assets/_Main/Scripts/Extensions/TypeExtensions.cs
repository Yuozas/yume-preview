using System;

public static class TypeExtensions
{
    /// <exception cref="ArgumentNullException"></exception>
    public static void RunGenericMethodForAllTypes<T>(this T typeToRunFrom, string methodName, params Type[] types)
        where T : class
    {
        if(types is null)
            throw new ArgumentNullException($"{nameof(types)} cannont be null.");

        var method = typeToRunFrom.GetType().GetMethod(methodName);
        foreach (var type in types)
            method.MakeGenericMethod(type).Invoke(typeToRunFrom, null);
    }
}
