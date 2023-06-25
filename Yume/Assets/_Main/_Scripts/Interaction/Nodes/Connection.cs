using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Connection
{
    [field: SerializeField] public string Text { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeReference] public List<INode> Nodes { get; private set; }

    private const string Default = "Choice";

    public Connection(string text = Default, List<INode> nodes = null)
    {
        Text = text;
        Nodes = nodes ?? new();
    }

    public void Execute()
    {
        foreach (var node in Nodes)
            node.Execute();
    }

    public void SetText(string text)
    {
        Text = text;
    }

    public void SetSprite(Sprite sprite)
    {
        Sprite = sprite;
    }

    public void Add(INode node)
    {
        if (!Contains(node))
            Nodes.Add(node);
    }

    public void Remove(INode node)
    {
        if (Contains(node))
            Nodes.Remove(node);
    }

    public bool Contains(INode node)
    {
        return Nodes.Contains(node);
    }
}