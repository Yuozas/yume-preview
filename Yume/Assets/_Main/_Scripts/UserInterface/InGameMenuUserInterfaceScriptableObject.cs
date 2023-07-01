using UnityEngine;
using UnityEngine.UIElements;

public abstract class InGameMenuUserInterfaceScriptableObject : ScriptableObject
{
    [field: SerializeField] private Sprite Icon { get; set; }
    [field: SerializeField] private VisualTreeAsset MenuElementTree { get; set; }

    protected VisualElement MenuElement { get; private set; }

    public void SetupMenuElement(VisualElement menuContent)
    {
        MenuElement = MenuElementTree.CloneTree().Q<VisualElement>("Body");
        menuContent.Add(MenuElement);
        SetupMenuElement();
    }

    public void SetupIcon(VisualElement menuIconContainer)
    {
        var icon = menuIconContainer.Q<VisualElement>("Icon");
        icon.style.backgroundImage = new StyleBackground(Icon);
    }

    protected abstract void SetupMenuElement();
}
