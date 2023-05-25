using TMPro;
using UnityEngine;

public class NameHandlerUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;

    private NameHandler _name;
    public void Initialize(NameHandler name)
    {
        _name = name;
        _name.OnUpdated += Set;

        Set(name.Text);
    }

    private void OnDestroy()
    {
        _name.OnUpdated -= Set;
    }

    private void Set(string name)
    {
        _text.SetText(name);
    }
}
