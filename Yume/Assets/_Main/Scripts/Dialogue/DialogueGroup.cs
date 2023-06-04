using System.Collections.Generic;
using System.Linq;

public class DialogueGroup
{
    public readonly IEnumerable<Dialogue> Dialogues;

    public DialogueGroup(IEnumerable<Dialogue> toggleables)
    {
        Dialogues = toggleables;
    }

    public Dialogue GetEnabled()
    {
        return Dialogues.Where(dialogue => dialogue.Toggler.Enabled).First();
    }
}
