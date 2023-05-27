using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;

public static class Initialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Initialize()
    {
        RegisterSingletonServices();
        ServiceLocator.Build();
    }

    private static void RegisterSingletonServices()
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

        var toggles = dialogues.Select(dialogue => dialogue.Toggle);
        _ = new Toggles(toggles);

        var resolver = new DialogueResolver(dialogues);


        ServiceLocator.SingletonRegistrator.Register(resolver);
    }
}

public interface ICommand
{
    void Execute();
}

public class SetName : ICommand
{
    readonly Name _name;

    public SetName(Name name)
    {
        _name = name;
    }
    public void Execute()
    {
        
    }
}