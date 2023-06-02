public class LeafNode : INode
{
    private readonly ICommand _executable;

    public LeafNode(ICommand executable)
    {
        _executable = executable;
    }

    public void Execute()
    {
        _executable.Execute();
    }
}

