using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;

    public Choice Choice { get; private set; }
    public int TextLength => Choice is null ? 0 : Choice.Text.Length;

    public void Initialize(Choice choice)
    {
        Choice = choice;
        _text.text = Choice.Text;

        _image.sprite = choice.Icon;
        _image.enabled = _image.sprite != null;
    }
}
