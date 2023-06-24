using System.Collections.Generic;

public class Choice
{
    public readonly string Text;
    private readonly List<INode> _nodes;

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