using UnityEngine;

public class DialogueUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TypewriterUserInterface _typewriter;
    [SerializeField] private PortraitUserInterface _portrait;
    [SerializeField] private NameUserInterface _name;

    private IToggler _toggle;

    public void Initialize(Dialogue dialogue)
    {
        _name?.Initialize(dialogue.Name);
        _portrait?.Initialize(dialogue.Portrait);
        _typewriter?.Initialize(dialogue.Typewriter);

        _toggle = dialogue.Toggler;
        _toggle.OnUpdated += Set;

        Set(_toggle.Enabled);
    }

    private void OnDestroy()
    {
        _toggle.OnUpdated -= Set;
    }

    private void Set(bool active)
    {
        gameObject.SetActive(active);
    }
}
