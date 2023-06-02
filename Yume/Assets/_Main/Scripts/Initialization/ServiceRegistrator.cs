using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class ServiceRegistrator : IPreliminarySetup
{
    public int Order => IPreliminarySetup.REGISTER;

    public void Setup()
    {
        RegisterDbServices();
    }

    private static void RegisterDbServices()
    {
        ServiceLocator.SingletonRegistrator
            .Register<IDbContext, RealmDbContext>();
        ServiceLocator.TransientRegistrator.Register<ITransientDbContext, RealmDbContext>();
    }
}
