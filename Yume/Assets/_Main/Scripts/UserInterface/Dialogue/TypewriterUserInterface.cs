using TMPro;
using UnityEngine;

public class TypewriterUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;

    private Typewriter _handler;

    public void Initialize(Typewriter handler)
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
