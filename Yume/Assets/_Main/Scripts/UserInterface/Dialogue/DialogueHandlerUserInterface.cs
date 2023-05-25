using UnityEngine;

public class DialogueHandlerUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TypewriterHandlerUserInterface _typewriter;
    [SerializeField] private PortraitHandlerUserInterface _portrait;
    [SerializeField] private NameHandlerUserInterface _name;

    private DialogueHandler _handler;

    public void Initialize(DialogueHandler handler)
    {
        _handler = handler;
        _handler.OnActiveBool += Set;

        _name?.Initialize(handler.Name);
        _portrait?.Initialize(handler.Portrait);
        _typewriter?.Initialize(handler.Typewriter);

        Set(_handler.Active);
    }

    private void OnDestroy()
    {
        _handler.OnActiveBool -= Set;
    }

    private void Set(bool active)
    {
        gameObject.SetActive(active);
    }
}
