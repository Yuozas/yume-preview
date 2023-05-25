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
            .Register<IDbContext, DbContext>(serviceProvider =>
            {
                const string databaseFileName = "Yume.db";

                var configuration = new DbConfigurations()
                {
                    DatabasePath = $"{Application.dataPath}/_Main/Databases/{databaseFileName}"
                };

                var dbContext = new DbContext(configuration, serviceProvider.Get<IDataStoreHelper>());
                dbContext.Build();
                return dbContext;
            })
            .Register<IMemoryCacheContext, MemoryCacheContext>()
            .Register<ICacheDbContext, CacheDbContext>()
            .Register<IDataStoreHelper, DataStoreHelper>();
    }
}
