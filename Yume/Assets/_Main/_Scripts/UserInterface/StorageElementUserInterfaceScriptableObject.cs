using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

// Todo. Refactor, this should not be a ScriptableObject.
public abstract class StorageElementUserInterfaceScriptableObject : ScriptableObject
{
    [field: SerializeField] private VisualTreeAsset MenuElementTree { get; set; }
    protected virtual bool MultipleContents { get; }

    protected VisualElement RootElement { get; private set; }

    private string _containerPath;

    public void SetupMenuElement(string containerPath, VisualElement root)
    { 
        _containerPath = containerPath;
        RootElement = root;

        if(MultipleContents)
            SetupMultipleContents();
        else
            SetupSingleContent();

        SetupMenuElement();
    }

    private void SetupSingleContent()
    {
        var content = MakeContent();
        GetContainers().First().Add(content);
    }

    private void SetupMultipleContents()
    {
        var containers = GetContainers();
        foreach (var container in containers)
            container.Add(MakeContent());
    }

    protected abstract void SetupMenuElement();

    private VisualElement MakeContent()
    {
        return MenuElementTree.CloneTree().Q<VisualElement>("Body");
    }

    private List<VisualElement> GetContainers()
    {
        var containers = RootElement.Query<VisualElement>(_containerPath).ToList();
        foreach (var container in containers)
            container.Clear();
        return containers;
    }
}