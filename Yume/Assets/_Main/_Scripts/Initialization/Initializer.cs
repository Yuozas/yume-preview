using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;

public class Initializer : IPreliminarySetup
{
    public void Setup()
    {
        ServiceLocator.SingletonRegistrator.Register(provider => {
            const string COROUTINES = "Coroutines";
            var gameObject = new GameObject(COROUTINES);
            Object.DontDestroyOnLoad(gameObject);

            return (MonoBehaviour)gameObject.AddComponent<Empty>();
        });

        ServiceLocator.SingletonRegistrator.Register(provider => {
            var factory = new DialogueFactory();
            var dialogues = new Dialogue[]
            {
                factory.BuildConversation(),
                factory.BuildInspection()
            };

            var toggles = dialogues.Select(dialogue => dialogue.Toggler);
            _ = new TogglerGroup(toggles);

            return new DialogueResolver(dialogues);
        });

        ServiceLocator.SingletonRegistrator.Register(provider => new Transitioner());
        ServiceLocator.SingletonRegistrator.Register(provider => new InSceneCharacter());

        ServiceLocator.SingletonRegistrator.Register(provider => {
            var prefab = Resources.Load<TransitionerAnimation>("TransitionerAnimation");
            var instantiated = Instantiator.InstantiateAndDontDestroy(prefab);
            return instantiated;
        });

        var prefab = Resources.Load<Music>("Music");
        var instantiated = Instantiator.InstantiateAndDontDestroy(prefab);
        ServiceLocator.SingletonRegistrator.Register(instantiated);
    }
}