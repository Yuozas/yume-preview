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

    public ChoiceGroup()
    {
        _choices = new();
        _index = 0;
    }

    public void Update(params Choice[] choices)
    {
        _choices.Clear();
        foreach (var choice in choices)
            _choices.Add(choice);

        Set(0);
        OnUpdated?.Invoke();
    }

    public void Next()
    {
        var index = _index - 1;
        if (index < 0)
            index = _choices.GetLastElementIndex();

        SetAndInvoke(index);
    }

    public void Previous()
    {
        var index = _index + 1;
        if (index >= _choices.Count)
            index = 0;

        SetAndInvoke(index);
    }

    public void Choose()
    {
        Selected.Choose();
    }

    private void SetAndInvoke(int index)
    {
        Set(index);
        OnSelected?.Invoke();
    }

    private void Set(int index)
    {
        _index = index;
        Selected = _choices[_index];
    }
}