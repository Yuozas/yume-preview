using System;
using System.Linq;

[Serializable]
public class ChoicesNode : BaseNode, INode
{
    public ICommand Executable { get; private set; }
    public ChoicesNode(string type, ICommand executable = null) : base(type)
    {
        Executable = executable ?? Executable;
    }

    public void Execute()
    {
        ValidateExecutable();
        Executable.Execute();
    }

    private void ValidateExecutable()
    {
        if (Executable is not null)
            return;

        var choices = Connections
            .Select(connection => new Choice(connection.Text, connection.Sprite, connection.Nodes))
            .ToArray();

        Executable = new SetChoicesCommand(choices);
    }
}