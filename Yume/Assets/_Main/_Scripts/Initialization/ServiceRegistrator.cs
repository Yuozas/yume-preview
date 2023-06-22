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
        ServiceLocator.SingletonRegistrator
            .Register<IRealmContext, RealmContext>()
            .Register<IRealmSaveManager, RealmSaveManager>(serviceProvider =>
            {
                var realmContext = serviceProvider.Get<IRealmContext>();
                var characterDataHandler = serviceProvider.Get<CharacterDataHandler>();
                var realmSaveRegistry = new RealmSaveRegistry(realmContext);
                return new RealmSaveManager(realmContext, realmSaveRegistry, characterDataHandler);
            })
            .Register<CharacterDataHandler>()
            .Register<SceneDataHandler>();
    }
}
