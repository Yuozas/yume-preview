using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;

public class Initializer : IPreliminarySetup
{
    private const string MUSIC_PREFAB_FILE_NAME = "Music";
    private const string SOUND_EFFECT_PREFAB_FILE_NAME = "SoundEffectAudioSource";
    private const string DIALOGUE_PREFAB_FILE_NAME = "DialoguesUserInterface";
    private const string SCENE_INTERACTIONS_FILE_PATH = "Interactions";
    private const string TRANSITIONER_ANIMATION_PREFAB_FILE_NAME = "TransitionerAnimation";
    private const string SLIDER_GAME_USER_INTERFACE_FILE_NAME = "SliderGameUserInterface";

    public void Setup()
    {
        if (!Application.isPlaying)
            return;

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
            var prefab = Resources.Load<TransitionerAnimation>(TRANSITIONER_ANIMATION_PREFAB_FILE_NAME);
            var instantiated = Instantiator.InstantiateAndDontDestroy(prefab);
            return instantiated;
        });

        ServiceLocator.SingletonRegistrator.Register(provider => {
            var prefab = Resources.Load<Music>(MUSIC_PREFAB_FILE_NAME);
            return Instantiator.InstantiateAndDontDestroy(prefab);
        });

        ServiceLocator.SingletonRegistrator.Register(provider => {
            var prefab = Resources.Load<SoundEffectAudioSource>(SOUND_EFFECT_PREFAB_FILE_NAME);
            return Instantiator.InstantiateAndDontDestroy(prefab);
        });

        ServiceLocator.SingletonRegistrator.Register(provider => {
            var prefab = Resources.Load<SliderGameUserInterface>(SLIDER_GAME_USER_INTERFACE_FILE_NAME);
            return Instantiator.InstantiateAndDontDestroy(prefab);
        });

        ServiceLocator.SingletonRegistrator.Register(provider => new Decisions());
        ServiceLocator.SingletonRegistrator.Register(provider =>
        {
            var prefab = Resources.Load<GameObject>("In-Game Menu Canvas");
            var instantiated = Instantiator.InstantiateAndDontDestroy(prefab);
            return instantiated.GetComponentInChildren<InGameMenuUserInterface>();
        });
        ServiceLocator.SingletonRegistrator.Register(provider =>
        {
            var prefab = Resources.Load<InGameMenuLaunchHandler>("In-game Menu Launcher");
            return Instantiator.InstantiateAndDontDestroy(prefab);
        });
        ServiceLocator.SingletonRegistrator.Register(provider =>
        {
            var prefab = Resources.Load<GameObject>("Storage Canvas");
            var instantiated = Instantiator.InstantiateAndDontDestroy(prefab);
            return instantiated.GetComponentInChildren<BackpackAndStorageUserInterface>();
        });

        var prefab = Resources.Load<DialoguesUserInterface>(DIALOGUE_PREFAB_FILE_NAME);
        Instantiator.InstantiateAndDontDestroy(prefab);

        var interactions = Resources.LoadAll<SceneInteractionScriptableObject>(SCENE_INTERACTIONS_FILE_PATH);
        _ = new SceneInteractionExecutor(interactions);

        ServiceLocator.SingletonRegistrator.Register(provider => new SliderGame());
        ServiceLocator.SingletonRegistrator.Register(provider =>
        {
            var introductionsQuest = Resources.Load<QuestScriptableObject>("Introductions_Quest");
            var investigateQuest = Resources.Load<QuestScriptableObject>("Investigate_House_Quest");
            return new Quests(introductionsQuest, investigateQuest);
        });

        ServiceLocator.SingletonRegistrator.Register(provider =>
        {
            var prefab = Resources.Load<QuestsUserInterface>("QuestsUserInterface");
            return Instantiator.InstantiateAndDontDestroy(prefab);
        });

        ServiceLocator.SingletonRegistrator.Register(provider => new Notifications());

        ServiceLocator.SingletonRegistrator.Register(provider =>
        {
            var prefab = Resources.Load<NotificationsUserInterface>("NotificationsUserInterface");
            return Instantiator.InstantiateAndDontDestroy(prefab);
        });
    }
}