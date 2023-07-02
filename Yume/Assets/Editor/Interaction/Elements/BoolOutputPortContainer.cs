using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections.Generic;

public class BoolOutputPortContainer : IDrawable
{
    public Action OnDrawn { get; set; }

    private readonly List<Connection> _connections;
    private readonly Type _type;

    private GraphNode _node;

    public BoolOutputPortContainer(INode node)
    {
        _connections = node.Connections;
        while(_connections.Count < 2)
            node.AddConnection();

        _type = typeof(bool);
    }

    public void Set(GraphNode node)
    {
        _node = node;
    }

    public void Draw()
    {
        foreach (var connection in _connections)
            CreateAndAddPort(_connections.IndexOf(connection) is 0 ? "True" : "False");

        OnDrawn?.Invoke();
    }

    public void CreateAndAddPort(string name)
    {
        var port = CreatePort(_type, name);
        _node.outputContainer.Add(port);
    }

    private Port CreatePort(Type type, string name)
    {
        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var port = _node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, type);
        port.portName = name;
        port.portColor = color;
        return port;
    }
}