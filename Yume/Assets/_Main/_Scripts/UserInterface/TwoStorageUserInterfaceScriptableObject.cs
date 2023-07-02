using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

// Todo. Refactor, this should not be a ScriptableObject.
[CreateAssetMenu(menuName = "Multiple Storage")]
public class TwoStorageUserInterfaceScriptableObject : StorageUserInterfaceScriptableObject
{
    private StorageSlot[] FirstSavedSlots { get; set; }
    private StorageSlot[] SecondSavedSlots { get; set; }
    private Action<StorageSlot> SlotClickedAction { get; set; }

    protected override bool MultipleContents => true;

    public void SetupFirstTitle(VisualElement menuTitleContainer, string title)
    {
        menuTitleContainer.Q<Label>("Title").text = title;
    }

    public void SetupSecondTitle(VisualElement menuTitleContainer, string title)
    {
        menuTitleContainer.Q<Label>("Title").text = title;
    }

    public void SetupFirstSavedSlots(IQueryable<StorageSlot> slots)
    {
        FirstSavedSlots = slots.GetDetatched();
    }

    public void SetupSecondSavedSlots(IQueryable<StorageSlot> slots)
    {
        SecondSavedSlots = slots.GetDetatched();
    }

    public void SetupOnSlotClick(Action<StorageSlot> clickedStorageSlot)
    {
        SlotClickedAction = clickedStorageSlot;
    }

    protected override void Setup()
    {
        var scrollView = RootElement.Query<ScrollView>("SlotScrollView").ToList();
        if (scrollView is not { Count: 2 })
            throw new ArgumentException("2 ScrollViews are expected.");

        Setup(scrollView[0], FirstSavedSlots);
        Setup(scrollView[1], SecondSavedSlots);
    }

    protected override void OnSlotClicked(VisualElement slot, StorageSlot storageSlot, StyleBackground itemBackgroundImage)
    {
        base.OnSlotClicked(slot, storageSlot, itemBackgroundImage);
        SlotClickedAction(storageSlot);
    }

    private void Setup(ScrollView scrollView, StorageSlot[] slots)
    {
        var slotContainer = GetSlotContainer();
        SetupScrollView(scrollView, slotContainer);
        SetupSlots(slotContainer, slots);
    }
}
