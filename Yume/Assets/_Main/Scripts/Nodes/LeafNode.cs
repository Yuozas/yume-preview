public class LeafNode : INode
{
    public readonly ICommand _executable;

    public LeafNode(ICommand executable)
    {
        _executable = executable;
    }

    public void Execute()
    {
        _executable.Execute();
    }
}

