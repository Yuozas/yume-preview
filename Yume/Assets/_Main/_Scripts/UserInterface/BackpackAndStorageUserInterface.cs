using SwiftLocator.Services.ServiceLocatorServices;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class BackpackAndStorageUserInterface : MonoBehaviour
{
    [field: SerializeField] private KeyboardKeyUserIterfaceBuilder KeyBuilder { get; set; }
    [field: SerializeField] private TwoStorageUserInterfaceScriptableObject TwoStorageUserInterfaceScriptableObject { get; set; }

    private InputActions _inputAction;
    private VisualElement _root;
    private VisualElement _backpackTitleContainer;
    private VisualElement _storageTitleContainer;

    private string ActiveStorageSlotId { get; set; }
    private string StorageId { get; set; }
    private string BackpackStorageId { get; set; }

    public void Enter(string storageId)
    {
        _inputAction.Storage.Enable();
        SubscribeInputs();

        Reload(storageId);

        _root.style.display = DisplayStyle.Flex;
    }

    public void Exit()
    {
        UnSubscribeInputs();
        _inputAction.Storage.Disable();

        ActiveStorageSlotId = null;

        _root.style.display = DisplayStyle.None;
    }

    private void Reload(string storageId)
    {
        TwoStorageUserInterfaceScriptableObject.SetupOnSlotClick(slot =>
        {
            ActiveStorageSlotId = slot.Id;
        });

        SetupStorage(storageId);
        SetupBackpack();

        TwoStorageUserInterfaceScriptableObject.SetupMenuElement("StorageContent", _root);
    }

    private void Awake()
    {
        _inputAction = new InputActions();

        _root = GetComponent<UIDocument>().rootVisualElement;
        var multipleStorages = _root.Q<VisualElement>("MultipleStorages");
        var panel = multipleStorages.Q<VisualElement>("Panel");

        var storageTitleAbsoluteContainers = panel.Query<VisualElement>("StorageTitleAbsoluteContainer").ToList();
        _backpackTitleContainer = storageTitleAbsoluteContainers[0];
        _storageTitleContainer = storageTitleAbsoluteContainers[1];

        var storagesButtonsContainer = panel.Q<VisualElement>("StoragesButtonsContainer");
        KeyBuilder.Build(storagesButtonsContainer);

        Exit();
    }

    private void SetupStorage(string storageId)
    {
        using var storage = ServiceLocator.GetSingleton<IStorageReadHelper>().GetStorage(storageId);
        TwoStorageUserInterfaceScriptableObject.SetupSecondSavedSlots(storage.Result.StorageSlots);

        StorageId = storage.Result.Id;

        var storageTitleContainer = _storageTitleContainer.Q<VisualElement>("StorageTitleWrapper");
        TwoStorageUserInterfaceScriptableObject.SetupSecondTitle(storageTitleContainer, storage.Result.Name);
    }

    private void SetupBackpack()
    {
        using var backpack = ServiceLocator.GetSingleton<IActiveCharacterHelper>().GetBackpack();
        TwoStorageUserInterfaceScriptableObject.SetupFirstSavedSlots(backpack.Result.StorageSlots);

        BackpackStorageId = backpack.Result.Id;

        var backpackTitleContainer = _backpackTitleContainer.Q<VisualElement>("StorageTitleWrapper");
        TwoStorageUserInterfaceScriptableObject.SetupFirstTitle(backpackTitleContainer, backpack.Result.Name);
    }

    private void SubscribeInputs()
    {
        _inputAction.Storage.Exit.performed += _ => Exit();
        _inputAction.Storage.Move.performed += _ => Move();
    }

    private void UnSubscribeInputs()
    {
        _inputAction.Storage.Exit.performed -= _ => Exit();
        _inputAction.Storage.Move.performed -= _ => Move();
    }

    private void Move()
    {
        if(ActiveStorageSlotId is null)
            return;

        using var storageSlot = ServiceLocator.GetSingleton<IStorageReadHelper>().GetStorageSlot(ActiveStorageSlotId);
        if(storageSlot.Result.Item is null)
            throw new Exception("Invalid exception. Item is null.");

        var storageItemHelper = ServiceLocator.GetSingleton<IStorageItemHelper>();

        if (storageSlot.Result.Storage.Id == BackpackStorageId)
            storageItemHelper.TryMoveFromTo(storageSlot.Result.Id, StorageId, storageSlot.Result.Item);
        else
            storageItemHelper.TryMoveFromTo(storageSlot.Result.Id, BackpackStorageId, storageSlot.Result.Item);

        Reload(StorageId);
    }
}
