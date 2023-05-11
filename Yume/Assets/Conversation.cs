using UnityEngine;

public class Conversation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private string[] _dialogue;

    private string _sentence => _dialogue[_index];
    private int _index;
    private DialogueBox _dialogueBox;

    private void Awake()
    {
        _dialogueBox = FindObjectOfType<DialogueBox>();
    }

    public void Interact()
    {
        if (_dialogue == null || _dialogue.Length <= 0) return;

        _index = 0;

        _dialogueBox.SetVisibility(true);
        _dialogueBox.Begin(_sentence, Next);
    }

    void Next()
    {
        if(_index >= _dialogue.Length - 1)
        {
            _dialogueBox.SetVisibility(false);
            return;
        }

        _index++;
        _dialogueBox.Begin(_sentence, Next);
    }
}
