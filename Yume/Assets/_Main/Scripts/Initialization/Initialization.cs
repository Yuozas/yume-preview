using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using SwiftLocator.Services.ScopedServices;

public static class Initialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Initialize()
    {
        RegisterSingletonServices();
        RegisterTransientServices();
        ServiceLocator.Build();
    }

    private static void RegisterSingletonServices()
    {
        // Todo register scoped services.
        //ServiceLocator.SingletonRegistrator;
    }

    private static void RegisterTransientServices()
    {
        // Todo register transient services.
        //ServiceLocator.TransientRegistrator;
    }
}
