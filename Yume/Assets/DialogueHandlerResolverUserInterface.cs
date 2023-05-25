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
}
