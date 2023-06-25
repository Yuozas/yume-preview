using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

public class MultiplePortContainer : IDrawable
{
    public Action OnDrawn { get; set; }

    public const string OUT_PORT_NAME = "Out";
    public const string IN_PORT_NAME = "In";

    private readonly string _name;
    private readonly Direction _direction;
    private readonly List<Connection> _connections;
    private GraphNode _node;
    private readonly Type _type;

    public MultiplePortContainer(string name, Direction direction, List<Connection> connections)
    {
        _connections = connections;
        _direction = direction;
        _name = name;
        _type = typeof(bool);
    }

    public void Set(GraphNode node)
    {
        _node = node;
    }

    public void Draw()
    {
        for (int i = 0; i < _connections.Count; i++)
        {
            var connection = _connections[i];
            var createDeleteButton = i is not 0;

            CreateAndAddPort(connection, createDeleteButton);
        }

        OnDrawn?.Invoke();
    }

    public void CreateAndAddPort(Connection connection, bool addDeleteButton)
    {
        var port = CreatePort(_type);
        var container = _direction is Direction.Output ? _node.outputContainer : _node.inputContainer;
        CreateAndAddTextField(port, connection);

        container.Add(port);
        var horizontalLine = new VisualElement();
        horizontalLine.style.height = 1;
        port.Add(horizontalLine);
        CreateAndAddIconField(connection, port);

        if (addDeleteButton)
            CreateAndAddDeleteButton(connection, port, container);
    }

    private void CreateAndAddIconField(Connection connection, Port port)
    {
        var field = new ObjectField()
        {
            objectType = typeof(Sprite),
            allowSceneObjects = false,
            value = connection.Sprite
        };

        AddStylingsToElement(field, "Dialogue/PortSpriteFieldStyles.uss", "sprite-field");

        field.RegisterValueChangedCallback(callback => 
            connection.SetSprite((Sprite)callback.newValue)
        );


        port.Add(field);
    }

    private void CreateAndAddTextField(Port port, Connection connection)
    {
        var field = new TextField() { value = connection.Text };
        field.RegisterValueChangedCallback(callback => connection.SetText(callback.newValue));


        AddStylingsToElement(field, "Dialogue/PortTextFieldStyles.uss", "text-field");

        port.Add(field);
    }

    private void CreateAndAddDeleteButton(Connection connection, Port port, VisualElement container)
    {
        var button = new Button() { text = "Delete" };
        button.clicked += () =>
        {
            _node.UnityNode.Node.RemoveConnection(connection);
            container.Remove(port);
        };

        port.Add(button);
    }

    private Port CreatePort(Type type)
    {
        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var port = _node.InstantiatePort(Orientation.Horizontal, _direction, Port.Capacity.Multi, type);
        port.portName = string.Empty;
        port.portColor = color;
        return port;
    }

    private void AddStylingsToElement(VisualElement element, string path, string @class)
    {
        var style = (StyleSheet)EditorGUIUtility.Load(path);
        element.styleSheets.Add(style);
        element.AddToClassList(@class);
    }
}