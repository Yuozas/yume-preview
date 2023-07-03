using System.Collections.Generic;
using UnityEngine;

public class Journal
{
    private readonly List<JournalEntryScriptableObject> _entries;

    public Journal(List<JournalEntryScriptableObject> quests = null)
    {
        _entries = quests ?? new List<JournalEntryScriptableObject>();
    }

    public void Add(JournalEntryScriptableObject entry)
    {
        var contains = Contains(entry);
        if (!contains)
            _entries.Add(entry);
    }

    public void Remove(JournalEntryScriptableObject entry)
    {
        var contains = Contains(entry);
        if (contains)
            _entries.Remove(entry);
    }

    private bool Contains(JournalEntryScriptableObject entry)
    {
        return _entries.Contains(entry);
    }
}

public abstract class JournalEntryScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    public bool Completed { get; private set; }

    public void SetCompleted()
    {
        Completed = true;
    }
}
