using System;
using System.Linq;

public class PurchaseManager : IPurchaseManager
{
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;
    private readonly IStorageItemHelper _storageItemHelper;
    private readonly IActiveCharacterHelper _activeCharacterHelper;
    private readonly Notifications _notifications;

    public PurchaseManager(IRealmActiveSaveHelper realmActiveSaveHelper, 
        IStorageItemHelper storageItemHelper, IActiveCharacterHelper activeCharacterHelper,
        Notifications notifications)
    {
        _realmActiveSaveHelper = realmActiveSaveHelper;
        _storageItemHelper = storageItemHelper;
        _activeCharacterHelper = activeCharacterHelper;
        _notifications = notifications;
    }

    public bool TryPurchase(StorageSlot storageSlot)
    {
        using var activeRealm = _realmActiveSaveHelper.GetActiveSave();
        using var transaction = activeRealm.StartTransaction();

        var activeCharacter = activeRealm.Get<ActiveCharacer>().Character;

        var purchasePrice = storageSlot.Prices.First().Price;

        if (activeCharacter.PossesedMoney < purchasePrice)
        {
            transaction.Rollback();
            return false;
        }

        activeRealm.WriteSafe(() => activeCharacter.PossesedMoney -= purchasePrice);

        var backpack = _activeCharacterHelper.GetBackpack();
        if (!_storageItemHelper.TryMoveFromTo(storageSlot.Id, backpack.Result.Id, storageSlot.Item, activeRealm))
        {
            transaction.Rollback();
            return false;
        }

        activeRealm.Remove(storageSlot);

        transaction.Commit();

        _notifications.Add(Notifications.INVENTORY_UPDATED);
        return true;
    }
}