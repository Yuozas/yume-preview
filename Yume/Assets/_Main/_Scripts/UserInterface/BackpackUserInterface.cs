using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BackpackUserInterface : MonoBehaviour
{
    private VisualElement _slotContainer;
    private VisualElement _slot;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        var backpack = body.Q<VisualElement>("Backpack");
        _slotContainer = backpack.Q<VisualElement>("SlotContainer");
        _slot = _slotContainer.Q<VisualElement>("Slot");

        SetupSlots();
    }

    private void SetupSlots()
    {
        using var activeRealm = ServiceLocator.GetSingleton<IRealmActiveSaveHelper>().GetActiveSave();
        var savedSlots = activeRealm.Get<ActiveCharacer>().Character.CharacterHasStorages.First().Storage.StorageSlots;

        var visualSlots = savedSlots.Select(SetupSlot).ToArray();
        _slotContainer.Clear();
        foreach (var slot in visualSlots)
            _slotContainer.Add(slot);
    }

    private VisualElement SetupSlot(StorageSlot storageSlot)
    {
        var slot = _slot.visualTreeAssetSource.CloneTree();
        var slotItem = slot.Q<Label>("SlotItem");

        if (storageSlot.Item is null)
            return slot;

        var texture = new Texture2D(2, 2);
        texture.LoadImage(storageSlot.Item.ItemIcon);

        slotItem.style.backgroundImage = new StyleBackground();

        return slot;
    }
}
