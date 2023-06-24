using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ChoicesNode : INode
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeField] public List<Connection> Connections { get; private set; }
    [field: SerializeReference] public ICommand Executable { get; private set; }

    public ChoicesNode(string type)
    {
        Type = type;
        Connections = Connections is null ? new() : Connections;

        var connection = new Connection();
        Add(connection);
    }

    public void Execute()
    {
        var choices = new Choice[Connections.Count];
        for (int i = 0; i < choices.Length; i++)
        {
            var connection = Connections[i];
            choices[i] = new Choice(connection.Text, connection.Nodes);
        }

        new SetChoicesCommand(choices).Execute();
    }

    public Connection Get(int index)
    {
        return Connections[index];
    }

    private bool Contains(Connection connection)
    {
        return Connections.Contains(connection);
    }

    private void Add(Connection connection)
    {
        var contains = Contains(connection);
        if (!contains)
            Connections.Add(connection);
    }
}