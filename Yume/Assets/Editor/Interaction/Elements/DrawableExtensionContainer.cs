using UnityEngine.UIElements;
using System;

public class DrawableExtensionContainer : IDrawable
{
    public Action OnDrawn { get; }

    private const string FOLDOUT_CLASS = "foldout-element";
    private const string FOLDOUT_CONTAINER_CLASS = "foldout-content";
    private const string EXTENSION_CONTAINER_CLASS = "extension-container";

    private GraphNode _node;
    private readonly VisualElement[] _addables;


    public DrawableExtensionContainer(params VisualElement[] addables)
    {
        _addables = addables;
    }

    public void Set(GraphNode node)
    {
        _node = node;
    }

    public void Draw()
    {
        var foldout = new Foldout();
        foldout.AddToClassList(FOLDOUT_CLASS);

        AddElements(foldout);

        var element = new VisualElement();
        element.Add(foldout);

        _node.extensionContainer.Add(element);
        _node.extensionContainer.AddToClassList(EXTENSION_CONTAINER_CLASS);

        OnDrawn?.Invoke();
    }

    private void AddElements(Foldout foldout)
    {
        var content = foldout.contentContainer;
        content.AddToClassList(FOLDOUT_CONTAINER_CLASS);

        foreach (var addable in _addables)
            content.Add(addable);
    }
}
