using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using System;
using System.Collections.Generic;

public class MonoInitialization : IPreliminarySetup
{
    public void Setup()
    {
        const string COROUTINES = "Coroutines";
        var gameObject = new GameObject(COROUTINES);
        var monoBehaviour = (MonoBehaviour)gameObject.AddComponent<Empty>();

        ServiceLocator.SingletonRegistrator.Register(monoBehaviour);

        var factory = new DialogueFactory();
        var dialogues = new Dialogue[]
        {
            factory.BuildConversation(),
            factory.BuildInspection()
        };

        var toggles = dialogues.Select(dialogue => dialogue.Toggler);
        _ = new TogglerGroup(toggles);

        var resolver = new DialogueResolver(dialogues);


        ServiceLocator.SingletonRegistrator.Register(resolver);
    }
}

[CreateAssetMenu(menuName = "Interaction")]
public class Interaction : ScriptableObject
{

}

public interface INode
{

}

[Serializable]
public class EntryNode : INode
{
    public const string NAME = "Entry";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public EntryNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}

[Serializable]
public class ExitNode : INode
{
    public const string NAME = "Exit";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public ExitNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}

public class EnableDialogueNode : INode
{
    public const string NAME = "Enable";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public EnableDialogueNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}

public class DisableDialogueNode : INode
{
    public const string NAME = "Disable";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public DisableDialogueNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}

public class DialogueNameSettingsNode : INode
{
    public const string NAME = "Name";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public DialogueNameSettingsNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}

public class DialoguePortraitSettingsNode : INode
{
    public const string NAME = "Portrait";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public DialoguePortraitSettingsNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}
public class DialogueTypewriterNode : INode
{
    public const string NAME = "Typewriter";

    public readonly NodeGroup Entry;
    public readonly NodeGroup Exit;
    public Vector3 Position;

    public DialogueTypewriterNode()
    {
        Entry = new NodeGroup();
        Exit = new NodeGroup();
    }
}

public class NodeGroup
{
    public readonly List<INode> Connections = new();

    public bool Contains(INode node)
    {
        return Connections.Contains(node);
    }

    public void Add(INode node)
    {
        var contains = Contains(node);
        if (!contains)
            Connections.Add(node);
    }

    public void Remove(INode node)
    {
        var contains = Contains(node);
        if (contains)
            Connections.Remove(node);
    }
}

