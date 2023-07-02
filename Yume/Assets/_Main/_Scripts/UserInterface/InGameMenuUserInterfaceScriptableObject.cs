using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "In Game Menu")]
public class InGameMenuUserInterfaceScriptableObject : ScriptableObject
{
    [field: SerializeField] private Sprite Icon { get; set; }
    [field: SerializeField] private StorageElementUserInterfaceScriptableObject StorageUserInterfaceScriptableObject { get; set; }

    public void SetupMenuElement(string menuContentContainerPath, VisualElement root)
    {
        StorageUserInterfaceScriptableObject.SetupMenuElement(menuContentContainerPath, root);
    }

    public void SetupIcon(VisualElement menuIconContainer)
    {
        var icon = menuIconContainer.Q<VisualElement>("Icon");
        icon.style.backgroundImage = new StyleBackground(Icon);
    }
}