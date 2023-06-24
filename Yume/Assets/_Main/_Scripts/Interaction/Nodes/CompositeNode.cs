using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CompositeNode : INode
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeReference] public List<Connection> Connections { get; private set; }
    [field: SerializeReference] public ICommand Executable { get; private set; }

    [SerializeField] private bool _wait;

    public CompositeNode(string type, bool wait, ICommand executable = null)
    {
        Type = type;
        Executable = executable;
        _wait = wait;

        Connections = Connections is null ? new() : Connections;
        AddDefaultConnection();
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
            Executable.Execute(Handle);
            return;
        }

        Executable.Execute();
        ExecuteAllConnections();
    }

    public void AddDefaultConnection()
    {
        if (Connections.Count > 0)
            return;

        var connection = new Connection();
        Add(connection);
    }

    public Connection Get(int index)
    {
        return Connections[index];
    }

    private void Add(Connection connection)
    {
        Connections.Add(connection);
    }

    private void Handle()
    {
        ExecuteConnection(0);
    }

    private void ExecuteConnection(int index)
    {
        Connections[index].Execute();
    }

    public void ExecuteAllConnections()
    {
        foreach (var connection in Connections)
            connection.Execute();
    }
}

[Serializable]
public class Connection
{
    [field: SerializeReference] public List<INode> Nodes { get; private set; }

    public Connection(List<INode> nodes = null)
    {
        Nodes = nodes ?? new();
    }

    public void Execute()
    {
        foreach (var node in Nodes)
            node.Execute();
    }

    public void Add(INode node)
    {
        var contains = Contains(node);
        if (!contains)
            Nodes.Add(node);
    }

    public void Remove(INode node)
    {
        var contains = Contains(node);
        if (contains)
            Nodes.Remove(node);
    }

    public bool Contains(INode node)
    {
        return Nodes.Contains(node);
    }
}