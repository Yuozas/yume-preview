using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.UIElements;

// Todo. Refactor, this should not be a ScriptableObject.
[CreateAssetMenu(menuName = "InGameMenu/Backpack")]
public class BackpackUserInterfaceScriptableObject : StorageUserInterfaceScriptableObject
{
    protected override void Setup()
    {
        var slotContainer = GetSlotContainer();
        var scrollView = RootElement.Q<ScrollView>("SlotScrollView");

        SetupScrollView(scrollView, slotContainer);

        using var backpack = ServiceLocator.GetSingleton<IActiveCharacterHelper>().GetBackpack();
        SetupSlots(slotContainer, backpack.Result.StorageSlots);
    }
}
