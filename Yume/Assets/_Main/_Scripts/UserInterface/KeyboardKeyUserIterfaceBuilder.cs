using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class KeyboardKeyUserIterfaceBuilder
{
    [field: SerializeField] private List<KeyboardKeyScriptableElement> Keys { get; set; }
    [field: SerializeField] private VisualTreeAsset KeyDocument { get; set; }

    private Dictionary<string, VisualElement> _elements;

    public void Build(VisualElement container)
    {
        _elements = new();
        foreach (var key in Keys)
        {
            var element = Build(key);
            _elements.Add(key.ActionName, element);
            container.Add(element);
        }
    }

    public void Hide(string actionName)
    {
        _elements[actionName].style.display = DisplayStyle.None;
    }

    public void Display(string actionName)
    {
        _elements[actionName].style.display = DisplayStyle.Flex;
    }

    private VisualElement Build(KeyboardKeyScriptableElement key)
    {
        var element = KeyDocument.CloneTree();
        var body = element.Q<VisualElement>("ButtonBody");
        var button = body.Q<VisualElement>("Button");

        var image = button.Q<VisualElement>("Image");
        image.style.backgroundImage = new StyleBackground(key.Sprite);

        var label = button.Q<Label>("ActionName");
        label.text = key.ActionName;
        return body;
    }
}