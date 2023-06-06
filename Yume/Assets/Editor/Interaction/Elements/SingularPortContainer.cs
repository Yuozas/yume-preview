using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SingularPortContainer : IDrawable
{
    public Action OnDrawn { get; set; }

    public const string OUT_PORT_NAME = "Out";
    public const string IN_PORT_NAME = "In";

    private readonly string _name;
    private readonly Direction _direction;
    private GraphNode _node;

    public SingularPortContainer(string name, Direction direction)
    {
        _direction = direction;
        _name = name;
    }

    public void Set(GraphNode node)
    {
        _node = node;
    }

    public void Draw()
    {
        var type = typeof(bool);

        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var output = _node.InstantiatePort(Orientation.Horizontal, _direction, Port.Capacity.Multi, type);
        output.portName = _name + "(Any)";
        output.portColor = color;

        _node.outputContainer.Add(output);
        OnDrawn?.Invoke();
    }
}
