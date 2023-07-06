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
            .Register<RealmSaveRegistry>()
            .Register<IRealmSaveManager, RealmSaveManager>()
            .Register<ISceneHelper, SceneHelper>()
            .Register<ICreateStorageHelper, CreateStorageHelper>()
            .Register<IStorageItemHelper, StorageItemHelper>()
            .Register<IRealmActiveSaveHelper, RealmActiveSaveHelper>()
            .Register<IRealmSaveReadHelper, RealmSaveReadHelper>()
            .Register<IActiveCharacterHelper, ActiveCharacterHelper>()
            .Register<IStorageReadHelper, StorageReadHelper>()
            .Register<IStorageManager, StorageManager>()
            .Register<IStoreManager, StoreManager>()
            .Register<IStoreReadHelper, StoreReadHelper>()
            .Register<IPurchaseManager, PurchaseManager>();
    }
}
