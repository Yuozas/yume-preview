using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BackpackUserInterface : MonoBehaviour
{
    [field: SerializeField] private VisualTreeAsset Slot { get; set; }
    [field: SerializeField] private VisualTreeAsset SlotContainer { get; set; }
    [field: SerializeField] private Sprite ScrollbarSprite { get; set; }
    [field: SerializeField] private Sprite ScrollbarLowButton { get; set; }
    [field: SerializeField] private Sprite ScrollbarHighButton { get; set; }
    [field: SerializeField] private Sprite ScrollbarDragger { get; set; }

    private ScrollView _scrollView;

    private VisualElement _slotContainer;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        var backpack = body.Q<VisualElement>("Backpack");
        _scrollView = backpack.Q<ScrollView>("SlotScrollView");

        SetupSlots();
        SetupScrollView();
    }

    private void SetupScrollView()
    {
        _scrollView.contentContainer.Add(_slotContainer);
        _scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        //_scrollView.verticalScroller.Q<VisualElement>("unity-dragger-border").style.display = DisplayStyle.None;
        //_scrollView.verticalScroller.Q<VisualElement>("unity-tracker").style.display = DisplayStyle.None;

        //var scrollViewSlider = _scrollView.verticalScroller.Q<VisualElement>("unity-slider");
        //scrollViewSlider.style.backgroundImage = new StyleBackground(ScrollbarSprite);

        //var scrollViewDragger = _scrollView.verticalScroller.Q<VisualElement>("unity-dragger");
        //scrollViewDragger.style.backgroundImage = new StyleBackground(ScrollbarDragger);
        //scrollViewDragger.style.height = 20;

        //var scrollViewLowButton = _scrollView.verticalScroller.Q<VisualElement>("unity-low-button");
        //scrollViewLowButton.style.backgroundImage = new StyleBackground(ScrollbarLowButton);

        //var scrollViewHighButton = _scrollView.verticalScroller.Q<VisualElement>("unity-high-button");
        //scrollViewHighButton.style.backgroundImage = new StyleBackground(ScrollbarHighButton);
    }

    private void SetupSlots()
    {
        _slotContainer = SlotContainer.CloneTree().Q("SlotContainer");

        using var activeRealm = ServiceLocator.GetSingleton<IRealmActiveSaveHelper>().GetActiveSave();
        var savedSlots = activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage.StorageSlots;

        var visualSlots = savedSlots.Select(SetupSlot).ToArray();
        foreach (var slot in visualSlots)
            _slotContainer.contentContainer.Add(slot);
    }

    private VisualElement SetupSlot(StorageSlot storageSlot)
    {
        var slot = Slot.CloneTree().Q("Slot");
        if (storageSlot.Item is null)
            return slot;

        var texture = new Texture2D(2, 2);
        texture.LoadImage(storageSlot.Item.ItemIcon);

        var slotItem = slot.Q<VisualElement>("SlotItem");
        slotItem.style.backgroundImage = new StyleBackground();

        return slot;
    }
}
