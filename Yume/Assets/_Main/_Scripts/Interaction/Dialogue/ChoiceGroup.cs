using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

public class ChoiceGroup
{
    public event Action OnSelected;
    public event Action OnUpdated;

    public Choice Selected { get; private set; }
    public ReadOnlyCollection<Choice> Choices => _choices.AsReadOnly();

    private readonly List<Choice> _choices;
    private int _index;

    public void Update(params Choice[] choices)
    {
        _choices.Clear();
        foreach (var choice in choices)
            _choices.Add(choice);

        OnUpdated?.Invoke();
    }

    public void Next()
    {
        var index = _index + 1;
        if (index >= _choices.Count)
            index = 0;

        Set(index);
    }

    public void Previous()
    {
        var index = _index - 1;
        if (index < 0)
            index = _choices.GetLastElementIndex();

        Set(index);
    }

    public void Choose()
    {
        Selected.Choose();
    }

    private void Set(int index)
    {
        _index = index;
        Selected = _choices[_index];
        OnSelected?.Invoke();
    }
}