using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SingularOutputPortContainer : IDrawable
{
    public Action OnDrawn { get; set; }

    public const string OUT_PORT_NAME = "Out";
    private GraphNode _node;

    public void Set(GraphNode node)
    {
        _node = node;
    }
    public void Draw()
    {
        var type = typeof(bool);

        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var output = _node.InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Output, Port.Capacity.Multi, type);
        output.portName = OUT_PORT_NAME + "(Any)";
        output.portColor = color;

        _node.outputContainer.Add(output);
        OnDrawn?.Invoke();
    }
}

