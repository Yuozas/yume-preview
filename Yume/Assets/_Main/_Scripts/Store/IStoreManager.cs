public interface IStoreManager
{
    RealmResult<Store> GetCreateStore(StoreScriptableObject storeScriptable);
}