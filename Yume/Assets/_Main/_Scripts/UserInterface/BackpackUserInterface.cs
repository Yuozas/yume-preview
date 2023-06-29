using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BackpackUserInterface : MonoBehaviour
{
    [field: SerializeField] Sprite SlotSprite { get; set; }

    private ScrollView _scrollView;

    private VisualElement _slotContainer;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        var backpack = body.Q<VisualElement>("Backpack");
        _scrollView = backpack.Q<ScrollView>("SlotScrollView");

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

        var texture = new Texture2D(2, 2);
        texture.LoadImage(storageSlot.Item.ItemIcon);

        var slotItem = slot.Q<VisualElement>("SlotItem");
        slotItem.style.backgroundImage = new StyleBackground();

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
