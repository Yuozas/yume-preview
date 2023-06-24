using System.Collections.Generic;

public interface INode
{
    public string Type { get; }

    public const string ENTRY = "Entry";
    public const string ENABLE = "Open";
    public const string DISABLE = "Close";
    public const string PORTRAIT = "Portrait";
    public const string MUSIC = "Music";
    public const string NAME = "Name";
    public const string TYPEWRITER = "Typewriter";
    public const string EXIT = "Exit";
    public const string SFX = "Sfx";
    public const string ENABLE_DECISIONS = "Open Decision";
    public const string DISABLE_DECISIONS = "Close Decision";
    public const string SET_CHOICES = "Set Choices";

    List<Connection> Connections { get; }
    ICommand Executable { get; }
    void Execute();
    Connection Get(int index);
}
