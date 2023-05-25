using System;
using System.Collections.Generic;
using System.Linq;

public class DialogueHandlerResolver
{
    private readonly List<DialogueHandler> _dialogues;

    public DialogueHandlerResolver(List<DialogueHandler> dialogues)
    {
        _dialogues = dialogues;
        for (int i = 0; i < dialogues.Count; i++)
            dialogues[i].OnActiveObject += Handle;
    }

    ~DialogueHandlerResolver()
    {
        for (int i = 0; i < _dialogues.Count; i++)
            _dialogues[i].OnActiveObject -= Handle;
    }

    public DialogueHandler Resolve(string type)
    {
        return _dialogues.First(dialogue => dialogue.Type == type);
    }

    private void Handle(bool active, DialogueHandler handler)
    {
        if (!active)
            return;

        SetActiveExcept(false, handler);
    }

    private void SetActiveExcept(bool active, DialogueHandler exception)
    {
        for (int i = 0; i < _dialogues.Count; i++)
        {
            var dialogue = _dialogues[i];
            if (dialogue == exception) continue;

            dialogue.SetActive(active);
        }
    }
}
