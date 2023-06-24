using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class BaseNode
{
    [field: SerializeField] public string Type { get; protected set; }
    [field: SerializeField] public List<Connection> Connections { get; protected set; }

    public BaseNode(string type)
    {
        Type = type;

        Connections = Connections is null ? new() : Connections;
        var connection = new Connection();
        Add(connection);
    }

    public Connection Get(int index)
    {
        return Connections[index];
    }

    private bool Contains(Connection connection)
    {
        return Connections.Contains(connection);
    }

    protected void Add(Connection connection)
    {
        var contains = Contains(connection);
        if (!contains)
            Connections.Add(connection);
    }
}