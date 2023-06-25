using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Choice
{
    [field: SerializeField] public string Text { get; private set; }
    [SerializeReference] private List<INode> _nodes;

    public Choice(string text, List<INode> nodes)
    {
        Text = text;
        _nodes = nodes;
    }

    public void Choose()
    {
        foreach (var node in _nodes)
            node.Execute();
    }
}