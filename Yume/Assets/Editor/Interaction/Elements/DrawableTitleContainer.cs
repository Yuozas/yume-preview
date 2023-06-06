using System;
using UnityEngine.UIElements;

public class DrawableTitleContainer : IDrawable
{
    public Action OnDrawn { get; set; }

    private const string TITLE_CLASS = "title-element";

    private readonly string _type;
    private GraphNode _node;

    public DrawableTitleContainer(string type)
    {
        _type = type;
    }

    public void Set(GraphNode node)
    {
        _node = node;
    }

    public void Draw()
    {
        var title = new Label(_type);
        title.AddToClassList(TITLE_CLASS);

        _node.titleContainer.Insert(0, title);
        OnDrawn?.Invoke();
    }
}

