using SwiftLocator.Services.ServiceLocatorServices;

public class ServiceRegistrator : IPreliminarySetup
{
    public int Order => IPreliminarySetup.REGISTER;

    public void Setup()
    {
        RegisterRealmServices();
    }

    private static void RegisterRealmServices()
    {
        ServiceLocator.SingletonRegistrator.Register<IRealmContext, RealmContext>();
    }
}
