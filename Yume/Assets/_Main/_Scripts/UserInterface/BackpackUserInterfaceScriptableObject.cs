using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "InGameMenu/Backpack")]
public class BackpackUserInterfaceScriptableObject : InGameMenuUserInterfaceScriptableObject
{
    [field: SerializeField] private Sprite SlotSprite { get; set; }

    private ScrollView _scrollView;
    private Label _itemName;
    private Label _itemDescription;
    private VisualElement _itemImage;
    private VisualElement _slotContainer;
    private VisualElement _activeSlot;

    protected override void SetupMenuElement()
    {
        _scrollView = MenuElement.Q<ScrollView>("SlotScrollView");
        _itemName = MenuElement.Q<Label>("ItemName");
        _itemDescription = MenuElement.Q<Label>("ItemDescription");
        _itemImage = MenuElement.Q<VisualElement>("ItemImage");

        SetupSlotContainer();
        SetupSlots();
        SetupScrollView();
    }

    private void SetupScrollView()
    {
        _scrollView.contentContainer.Add(_slotContainer);
        _scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
    }

    private void SetupSlotContainer()
    {
        _slotContainer = new VisualElement
        {
            name = "SlotContainer"
        };
    }

    private void SetupSlots()
    {
        using var activeRealm = ServiceLocator.GetSingleton<IRealmActiveSaveHelper>().GetActiveSave();
        var savedSlots = activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage.StorageSlots;

        var visualSlots = savedSlots.Select(SetupSlot).ToArray();
        foreach (var slot in visualSlots)
            _slotContainer.contentContainer.Add(slot);
    }

    private VisualElement SetupSlot(StorageSlot storageSlot)
    {
        var slot = CreateSlotVisualElement();
        if (storageSlot.Item is null)
            return slot;

        var image = storageSlot.Item.ItemIcon?.ConvertByteArrayToSprite();
        var itemBackgroundImage = _itemImage.style.backgroundImage = image == null
            ? null
            : new StyleBackground(image);

        slot.AddManipulator(new Clickable(() =>
        {
            _activeSlot?.RemoveFromClassList("active-slot");
            _activeSlot = slot;
            _activeSlot.AddToClassList("active-slot");
            _itemName.text = storageSlot.Item.Name;
            _itemDescription.text = storageSlot.Item.Description;
            _itemImage.style.backgroundImage = itemBackgroundImage;
        }));

        var texture = new Texture2D(2, 2);
        texture.LoadImage(storageSlot.Item.ItemIcon);

        var slotItem = slot.Q<VisualElement>("SlotItem");
        slotItem.style.backgroundImage = itemBackgroundImage;

        return slot;
    }

    private VisualElement CreateSlotVisualElement()
    {
        var slot = new VisualElement
        {
            name = "Slot"
        };

        slot.style.backgroundImage = new StyleBackground(SlotSprite);

        slot.contentContainer.Add(new VisualElement()
        {
            name = "SlotItem"
        });

        return slot;
    }
}
