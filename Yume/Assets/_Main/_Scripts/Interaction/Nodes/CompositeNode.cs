using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CompositeNode : INode
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeField] public List<Connection> Connections { get; private set; }
    [field: SerializeReference] public ICommand Executable { get; private set; }

    [SerializeField] private bool _wait;

    public CompositeNode(string type, bool wait, ICommand executable = null)
    {
        Type = type;
        Executable = executable;
        _wait = wait;

        Connections = Connections is null ? new() : Connections;
        var connection = new Connection();
        Add(connection);
    }

    public void Execute()
    {
        if(Executable is null)
        {
            ExecuteAllConnections();
            return;
        }

        if (_wait)
        {
            Executable.Execute(ExecuteAllConnections);
            return;
        }

        Executable.Execute();
        ExecuteAllConnections();
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
        if(!contains)
            Connections.Add(connection);
    }

    private void ExecuteAllConnections()
    {
        foreach (var connection in Connections)
            connection.Execute();
    }
}