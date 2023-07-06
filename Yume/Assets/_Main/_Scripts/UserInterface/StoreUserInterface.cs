using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreUserInterface : MonoBehaviour
{
    [field: SerializeField] private VisualTreeAsset ItemContainer { get; set; }
    [field: SerializeField] private VisualTreeAsset PossesedMoneyContainer { get; set; }

    private VisualElement _root;
    private Label _itemCostLabel;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        Exit();
    }

    public void Enter(string storeId)
    {
        _root.style.display = DisplayStyle.Flex;

        var store = ServiceLocator.GetSingleton<IStoreReadHelper>().GetStore(storeId);

        SetupItems(store.Result.Storages.FirstOrDefault().Storage.StorageSlots, storeId);
        SetupItemCost();
        SetupCharacterPossesedMoney();
        SetupStoreTitle(store.Result);
    }

    private void SetupStoreTitle(Store store)
    {
        _root.Q<VisualElement>("StoreTitleContainer")
            .Q<Label>("StoreTitle")
            .text = store.Name;
    }

    private void SetupItemCost()
    {
        var possesedMoneyContainer = _root.Q<VisualElement>("Details").Q<VisualElement>("ItemPrice");
        possesedMoneyContainer.contentContainer.Clear();

        var moneyContainerTree = PossesedMoneyContainer.CloneTree();
        var moneyContainer = moneyContainerTree.Q<VisualElement>("PossesedMoney");

        _itemCostLabel = moneyContainer.Q<Label>("Amount");
        _itemCostLabel.text = "0";

        possesedMoneyContainer.contentContainer.Add(moneyContainer);
    }

    private void SetupCharacterPossesedMoney()
    {
        var itemPriceContainer = _root.Q<VisualElement>("PossedMoneyContainer");
        itemPriceContainer.contentContainer.Clear();

        var moneyContainerTree = PossesedMoneyContainer.CloneTree();
        var moneyContainer = moneyContainerTree.Q<VisualElement>("PossesedMoney");

        var money = ServiceLocator.GetSingleton<IActiveCharacterHelper>().GetPossesedMoney();
        var possesedMoeny = moneyContainer.Q<Label>("Amount");
        possesedMoeny.text = money.ToString();

        itemPriceContainer.contentContainer.Add(moneyContainer);
    }

    private void SetupItems(IQueryable<StorageSlot> storageSlots, string storeId)
    {
        var itemsContainer = _root.Q<VisualElement>("ItemsContainer");
        var scrollView = itemsContainer.Q<ScrollView>("ScrollView");
        scrollView.contentContainer.Clear();

        foreach (var storageSlot in storageSlots)
        {
            var itemContainer = ItemContainer.CloneTree();
            itemContainer.AddManipulator(new Clickable(() => DisplayItemPrice(storageSlot.Prices.First())));
            itemContainer.Q<VisualElement>("Image").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(storageSlot.Item.IconPath));
            itemContainer.Q<Label>("ItemName").text = storageSlot.Item.Name;
            itemContainer.Q<VisualElement>("ItemBuyButton").AddManipulator(new Clickable(() => Purchase(storageSlot.Prices.First(), storeId)));
            scrollView.contentContainer.Add(itemContainer);
        }
    }

    private void DisplayItemPrice(StorageSlotHasPrice storageSlotHasPrice)
    {
        _itemCostLabel.text = storageSlotHasPrice.Price.ToString();
    }

    private void Purchase(StorageSlotHasPrice storageSlotHasPrice, string storeId)
    {
        if (!ServiceLocator.GetSingleton<IPurchaseManager>().TryPurchase(storageSlotHasPrice.StorageSlot))
            return;
        Enter(storeId);
    }

    public void Exit()
    {
        _root.style.display = DisplayStyle.None;
    }
}
