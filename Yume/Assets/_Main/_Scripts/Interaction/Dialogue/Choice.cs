using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Choice
{
    [field: SerializeField] public string Text { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [SerializeReference] private List<INode> _nodes;

    public Choice(string text, Sprite sprite, List<INode> nodes)
    {
        Text = text;
        Icon = sprite;
        _nodes = nodes;
    }

    public void Choose()
    {
        foreach (var node in _nodes)
            node.Execute();
    }
}