using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class PreliminarySetup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static async void Setup()
    {
        if(SceneManager.GetActiveScene().buildIndex is not 0)
            await SceneManager.LoadSceneAsync(0);

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
