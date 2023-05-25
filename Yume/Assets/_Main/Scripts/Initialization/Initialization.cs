using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using System.Collections.Generic;

public static class Initialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Initialize()
    {
        //Load Initialization Scene.
        //Coroutine Monobehaviour.

        RegisterSingletonServices();
        RegisterTransientServices();
        ServiceLocator.Build();
    }

    private static void RegisterSingletonServices()
    {
        const string COROUTINES = "Coroutines";
        var gameObject = new GameObject(COROUTINES);
        var monoBehaviour = (MonoBehaviour)gameObject.AddComponent<Empty>();

        ServiceLocator.SingletonRegistrator.Register(monoBehaviour);

        var dialogues = new List<DialogueHandler>
        {
            CreateDialogueHandler(DialogueHandler.INSPECTION, true, false, false),
            CreateDialogueHandler(DialogueHandler.CONVERSATION, true, true, true)
        };

        var dialogueManager = new DialogueHandlerResolver(dialogues);
        
        ServiceLocator.SingletonRegistrator.Register(dialogueManager);
    }

    private static DialogueHandler CreateDialogueHandler(string type, bool? useTypewriter, bool? usePortrait, bool? useName)
    {
        var typewriter = useTypewriter != null ? CreateTypewriter() : null;
        var portrait = usePortrait != null ? new PortraitHandler() : null;
        var name = useName != null ? new NameHandler() : null;

        return new DialogueHandler(type, typewriter, portrait, name);
    }

    private static TypewriterHandler CreateTypewriter()
    {
        var delayedExecutor = new DelayedExecutor();

        var typewriterIterator = new TypewriterIterator();
        var typewriterHandler = new TypewriterHandler(delayedExecutor, typewriterIterator);
        return typewriterHandler;
    }

    private static void RegisterTransientServices()
    {
        // Todo register transient services.
        //ServiceLocator.TransientRegistrator;
    }
}
