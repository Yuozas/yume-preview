using TMPro;
using UnityEngine;

public class NameUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;

    private Name _name;
    public void Initialize(Name name)
    {
        _name = name;
        _name.OnUpdated += Set;

        Set(name.Settings);
    }

    private void OnDestroy()
    {
        _name.OnUpdated -= Set;
    }

    private void Set(NameSettings settings)
    {
        _text.SetText(settings.Name);
    }
}
