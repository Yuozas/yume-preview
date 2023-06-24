using System.Collections.Generic;
using System;
using UnityEngine;

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