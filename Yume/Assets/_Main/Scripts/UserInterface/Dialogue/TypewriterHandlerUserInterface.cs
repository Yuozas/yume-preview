using TMPro;
using UnityEngine;

public class TypewriterHandlerUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;

    private TypewriterHandler _handler;

    public void Initialize(TypewriterHandler handler)
    {
        _handler = handler;
        _handler.OnUpdated += Set;

        _text.text = string.Empty;
    }

    private void OnDestroy()
    {
        _handler.OnUpdated -= Set;
    }

    private void Set(string name)
    {
        _text.SetText(name);
    }
}
