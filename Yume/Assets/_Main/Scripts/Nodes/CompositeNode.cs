using System.Collections.Generic;

public class CompositeNode : INode
{
    private readonly bool _wait;

    private readonly ICommand _executable;
    private readonly List<INode> _nodes;

    public CompositeNode(ICommand executable, bool wait, List<INode> nodes = null)
    {
        _executable = executable;
        _wait = wait;

        _nodes = nodes ?? new List<INode>();
    }

    public void Execute()
    {
        if (_wait)
        {
            _executable.Execute(Continue);
            return;
        }

        _executable.Execute();
        Continue();
    }

    public void Add(INode node)
    {
        var contains = Contains(node);
        if (!contains)
            _nodes.Add(node);
    }

    public void Remove(INode node)
    {
        var contains = Contains(node);
        if (contains)
            _nodes.Remove(node);
    }

    public bool Contains(INode node)
    {
        return _nodes.Contains(node);
    }

    private void Continue()
    {
        foreach (var node in _nodes)
            node.Execute();
    }
}

