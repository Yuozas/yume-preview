using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SingularInputPortContainer : IDrawable
{
    public Action OnDrawn { get; set; }

    public const string IN_PORT_NAME = "In";
    private GraphNode _node;

    public void Set(GraphNode node)
    {
        _node = node;
    }
    public void Draw()
    {
        var type = typeof(bool);

        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var input = _node.InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Input, Port.Capacity.Multi, type);
        input.portName = IN_PORT_NAME + "(Any)";
        input.portColor = color;

        _node.inputContainer.Add(input);
        OnDrawn?.Invoke();
    }
}

