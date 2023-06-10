using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public static class PreliminarySetup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Setup()
    {
        var setups = GetAllPreliminarySetups().OrderBy(setup => setup.Order);
        foreach (var setup in setups)
            setup.Setup();
    }

    private static IEnumerable<IPreliminarySetup> GetAllPreliminarySetups()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        foreach (Type type in types)
            if (typeof(IPreliminarySetup).IsAssignableFrom(type) && !type.IsInterface)
                yield return Activator.CreateInstance(type) as IPreliminarySetup;
    }
}
