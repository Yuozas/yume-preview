using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CompositeNode : INode
{
    [field: SerializeField] public string Type { get; private set; }
    [field: SerializeReference] public List<INode> Connections { get; private set; }

    [SerializeField] private bool _wait;
    [field: SerializeReference] public ICommand Executable { get; private set; }


    public CompositeNode(string type, bool wait, ICommand executable = null, List<INode> nodes = null)
    {
        Type = type;
        Executable = executable;
        _wait = wait;

        Connections = nodes ?? new();
    }

    public void Execute()
    {
        if(Executable == null)
        {
            Continue();
            return;
        }

        if (_wait)
        {
            Executable.Execute(Continue);
            return;
        }

        Executable.Execute();
        Continue();
    }

    public void Add(INode node)
    {
        var contains = Contains(node);
        if (!contains)
            Connections.Add(node);
    }

    public void Remove(INode node)
    {
        var contains = Contains(node);
        if (contains)
            Connections.Remove(node);
    }

    public bool Contains(INode node)
    {
        return Connections.Contains(node);
    }

    private void Continue()
    {
        foreach (var node in Connections)
            node.Execute();
    }
}

