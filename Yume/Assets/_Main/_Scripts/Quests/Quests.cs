using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Quests
{
    public event Action<QuestScriptableObject> OnAdded;
    public readonly Toggler Toggler;
    public ReadOnlyCollection<QuestScriptableObject> Entries => _entries.AsReadOnly();
    
    private readonly List<QuestScriptableObject> _entries;


    public Quests(params QuestScriptableObject[] quests)
    {
        _entries = quests.ToList();
        foreach (var entry in _entries)
            entry.Initialize();

        Toggler = new Toggler();
    }

    public void Add(QuestScriptableObject entry)
    {
        var contains = Contains(entry);
        if (!contains)
        {
            _entries.Add(entry);
            OnAdded?.Invoke(entry);
        }
    }

    public void Remove(QuestScriptableObject entry)
    {
        var contains = Contains(entry);
        if (contains)
            _entries.Remove(entry);
    }

    private bool Contains(QuestScriptableObject entry)
    {
        return _entries.Contains(entry);
    }
}