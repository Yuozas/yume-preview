using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Quests
{
    public event Action<QuestScriptableObject> OnAdded;
    public readonly Toggler Toggler;
    public ReadOnlyCollection<QuestScriptableObject> Entries => _quests.AsReadOnly();
    public event Action<int> OnSelectedQuestIndexUpdated;
    
    private readonly List<QuestScriptableObject> _quests;
    private readonly Notifications _notifications;
    private int _index;

    public Quests(params QuestScriptableObject[] quests)
    {
        _quests = quests.ToList();
        foreach (var entry in _quests)
            entry.Initialize();

        Toggler = new Toggler();
    }

    public void Next()
    {
        _index++;
        if (_index >= _quests.Count)
            _index = 0;

        OnSelectedQuestIndexUpdated?.Invoke(_index);
    }

    public void Previous()
    {
        _index--;
        if (_index < 0)
            _index = _quests.Count - 1;

        OnSelectedQuestIndexUpdated?.Invoke(_index);
    }

    public void Add(QuestScriptableObject entry)
    {
        var contains = Contains(entry);
        if (!contains)
        {
            _notifications.Add("Quests updated");
            _quests.Add(entry);
            OnAdded?.Invoke(entry);
        }
    }

    public void Remove(QuestScriptableObject entry)
    {
        var contains = Contains(entry);
        if (contains)
            _quests.Remove(entry);
    }

    private bool Contains(QuestScriptableObject entry)
    {
        return _quests.Contains(entry);
    }
}