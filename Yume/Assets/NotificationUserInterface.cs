using UnityEngine;
using TMPro;

public class NotificationUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;

    public void Initialize(string name)
    {
        _text.text = name;
    }
}
