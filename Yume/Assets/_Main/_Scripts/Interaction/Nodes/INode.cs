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
    public const string PLAY_SOUND_EFFECT = "Play Sound Effect";
    public const string ENABLE_DECISIONS = "Open Decision";
    public const string DISABLE_DECISIONS = "Close Decision";
    public const string SET_DECISION_CHOICES = "Set Decision Choices";
    public const string TRANSITION_TO_DESTINATION = "Transition To Destination";

    List<Connection> Connections { get; }
    ICommand Executable { get; }
    void Execute();
    Connection Get(int index);
    Connection AddConnection();
    void RemoveConnection(Connection connection);
}
