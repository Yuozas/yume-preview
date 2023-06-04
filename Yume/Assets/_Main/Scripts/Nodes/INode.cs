using System.Collections.Generic;

public interface INode
{
    public string Type { get; }

    public const string ENTRY = "Entry";
    public const string ENABLE = "Open";
    public const string DISABLE = "Close";
    public const string PORTRAIT = "Portrait";
    public const string NAME = "Name";
    public const string TYPEWRITER = "Typewriter";
    public const string EXIT = "Exit";

    List<INode> Connections { get; }
    ICommand Executable { get; }
    void Add(INode node);
    void Remove(INode node);
    void Execute();
}
