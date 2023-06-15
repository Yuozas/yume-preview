using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class DialoguesUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueUserInterface _conversation;
    [SerializeField] private DialogueUserInterface _inspection;

    private void Awake()
    {
        var resolver = ServiceLocator.GetSingleton<DialogueResolver>();
        var conversation = resolver.Resolve(Dialogue.CONVERSATION);
        var inspection = resolver.Resolve(Dialogue.INSPECTION);

        _conversation.Initialize(conversation);
        _inspection.Initialize(inspection);
    }
}
