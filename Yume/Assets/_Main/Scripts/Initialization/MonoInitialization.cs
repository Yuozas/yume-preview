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
        var dialogueGroup = new DialogueGroup(dialogues);

        var resolver = new DialogueResolver(dialogues);

        ServiceLocator.SingletonRegistrator.Register(resolver);
        ServiceLocator.SingletonRegistrator.Register(dialogueGroup);
    }
}