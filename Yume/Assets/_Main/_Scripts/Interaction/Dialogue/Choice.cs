public class Choice
{
    private readonly string _text;
    private readonly INode _node;

    public Choice(string text, INode node)
    {
        _text = text;
        _node = node;
    }

    public void Choose()
    {
        _node.Execute();
    }
}