using SwiftLocator.Services.ServiceLocatorServices;

public class PreliminaryDbInitialiezer : IPreliminarySetup
{
    public int Order => IPreliminarySetup.USE;

    public void Setup()
    {
        ServiceLocator.SingletonProvider.Get<ICacheDbContext>().CacheAllDataFromDb();
    }
}
