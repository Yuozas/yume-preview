using System.Collections.Generic;
using System.Linq;

public class DialogueResolver
{
    private readonly IEnumerable<Dialogue> _dialogues;

    public DialogueResolver(IEnumerable<Dialogue> dialogues)
    {
        _dialogues = dialogues;
    }

    public Dialogue[] Resolve()
    {
        return _dialogues.ToArray();
    }

    public Dialogue Resolve(string type)
    {
        return _dialogues.First(dialogue => dialogue.Type == type);
    }

    public Dialogue ResolveWhereTogglerEnabled()
    {
        return _dialogues.First(dialogue => dialogue.Toggler.Enabled);
    }
}