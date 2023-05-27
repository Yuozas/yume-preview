using System.Collections.Generic;
using System.Linq;

public class DialogueResolver
{
    protected readonly IEnumerable<Dialogue> _dialogues;

    public DialogueResolver(IEnumerable<Dialogue> dialogues)
    {
        _dialogues = dialogues;
    }

    public Dialogue Resolve(string type)
    {
        return _dialogues.First(dialogue => dialogue.Type == type);
    }
}
