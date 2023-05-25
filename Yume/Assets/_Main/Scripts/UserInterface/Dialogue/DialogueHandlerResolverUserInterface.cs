using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class DialogueHandlerResolverUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueHandlerUserInterface _conversation;
    [SerializeField] private DialogueHandlerUserInterface _inspection;

    private void Awake()
    {
        var resolver = ServiceLocator.GetSingleton<DialogueHandlerResolver>();
        var conversation = resolver.Resolve(DialogueHandler.CONVERSATION);
        var inspection = resolver.Resolve(DialogueHandler.INSPECTION);

        _conversation.Initialize(conversation);
        _inspection.Initialize(inspection);
    }

    private void Update()
    {
        var resolver = ServiceLocator.GetSingleton<DialogueHandlerResolver>();

        var zPressed = Input.GetKeyDown(KeyCode.Z);
        if (zPressed)
        {
            var inspection = resolver.Resolve(DialogueHandler.INSPECTION);
            inspection.SetActive(!inspection.Active);

            inspection.Typewriter.Execute(TypewriterSettings.DEFAULT);
        }

        var xPressed = Input.GetKeyDown(KeyCode.X);
        if (xPressed)
        {
            var conversation = resolver.Resolve(DialogueHandler.CONVERSATION);
            conversation.SetActive(!conversation.Active);

            conversation.Typewriter.Execute(TypewriterSettings.DEFAULT);
        }
    }
}
