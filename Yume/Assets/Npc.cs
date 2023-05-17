using UnityEngine;
using System.Collections.Generic;

public class Npc : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private string[] _dialogue;

    private string _sentence => _dialogue[_index];
    private int _index;
    private DialogueBox _interface;

    protected override void Awake()
    {
        base.Awake();

        _interface = FindObjectOfType<DialogueBox>();
    }

    public void Interact()
    {
        if (_dialogue == null || _dialogue.Length <= 0) return;

        _index = 0;

        _interface.SetVisibility(true);
        _interface.Begin(_sentence, Next);
    }

    void Next()
    {
        if (_index >= _dialogue.Length - 1)
        {
            _interface.SetVisibility(false);
            return;
        }

        _index++;
        _interface.Begin(_sentence, Next);
    }
}


public class Dialogue : ScriptableObject
{

}


//[CreateAssetMenu(menuName = "Sentence")]
//public class Sentence : ScriptableObject
//{
//    public string Sentence;
//}