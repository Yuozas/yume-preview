using UnityEngine;

public class DialogueHandlerUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TypewriterHandlerUserInterface _typewriter;
    [SerializeField] private PortraitHandlerUserInterface _portrait;
    [SerializeField] private NameHandlerUserInterface _name;

    public void Initialize(DialogueHandler handler)
    {
        _name?.Initialize(handler.Name);
        _portrait?.Initialize(handler.Portrait);
        _typewriter?.Initialize(handler.Typewriter);
    }
}
