using SwiftLocator.Services.ServiceLocatorServices;

public class PreliminaryDbInitialiezer : IPreliminarySetup
{
    public int Order => IPreliminarySetup.USE;

    public void Setup()
    {
        // Cache database data to memory.
        ServiceLocator.SingletonProvider.Get<ICacheDbContext>().CacheAllDataFromDb();
    }
}
