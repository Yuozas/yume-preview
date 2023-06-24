using UnityEngine;
using TMPro;

public class ChoiceUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;

    public Choice Choice { get; private set; }

    public void Initialize(Choice choice)
    {
        Choice = choice;
        _text.text = Choice.Text;
    }
}
