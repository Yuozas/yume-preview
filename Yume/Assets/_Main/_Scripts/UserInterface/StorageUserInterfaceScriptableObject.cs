using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class StorageUserInterfaceScriptableObject : StorageElementUserInterfaceScriptableObject
{
    [field: SerializeField] private Sprite SlotSprite { get; set; }

    protected Label ItemName;
    protected Label ItemDescription;
    protected VisualElement ItemImage;
    protected VisualElement ItemDetails;
    protected VisualElement ActiveSlot;

    protected override void SetupMenuElement()
    {
        SetupItemDescription();
        Setup();
    }

    protected VisualElement GetSlotContainer()
    {
        return new VisualElement { name = "SlotContainer" };
    }

    protected virtual void OnSlotClicked(VisualElement slot, StorageSlot storageSlot, StyleBackground itemBackgroundImage)
    {
        ItemDetails.style.display = DisplayStyle.Flex;

        ActiveSlot?.RemoveFromClassList("active-slot");
        ActiveSlot = slot;
        ActiveSlot.AddToClassList("active-slot");
        ItemName.text = storageSlot.Item.Name;
        ItemDescription.text = storageSlot.Item.Description;
        ItemImage.style.backgroundImage = itemBackgroundImage;
    }

    protected virtual void SetupItemDescription()
    {
        ItemDetails = RootElement.Q<VisualElement>("ItemDetails");
        ItemName = ItemDetails.Q<Label>("ItemName");
        ItemDescription = ItemDetails.Q<Label>("ItemDescription");
        ItemImage = ItemDetails.Q<VisualElement>("ItemImage");
        ItemDetails.style.display = DisplayStyle.None;
    }

    protected abstract void Setup();

    protected void SetupScrollView(ScrollView scrollView, VisualElement slotContainer)
    {
        scrollView.contentContainer.Clear();
        scrollView.contentContainer.Add(slotContainer);
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
    }

    protected void SetupSlots(VisualElement slotContainer, StorageSlot[] slots)
    {
        var visualSlots = slots.Select(SetupSlot).ToArray();
        foreach (var slot in visualSlots)
            slotContainer.contentContainer.Add(slot);
    }

    private VisualElement SetupSlot(StorageSlot storageSlot)
    {
        var slot = CreateSlotVisualElement();
        if (storageSlot.Item is null)
            return slot;

        var image = Resources.Load<Sprite>(storageSlot.Item.IconPath);

        var itemBackgroundImage = image == null
            ? null
            : new StyleBackground(image);

        slot.AddManipulator(new Clickable(() => OnSlotClicked(slot, storageSlot, itemBackgroundImage)));

        var slotItem = slot.Q<VisualElement>("SlotItem");
        slotItem.style.backgroundImage = itemBackgroundImage;

        return slot;
    }

    private VisualElement CreateSlotVisualElement()
    {
        var slot = new VisualElement { name = "Slot" };

        slot.style.backgroundImage = new StyleBackground(SlotSprite);
        slot.contentContainer.Add(new VisualElement() { name = "SlotItem" });

        return slot;
    }
}
