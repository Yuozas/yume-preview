using System;
using UnityEngine;

public abstract class QuestScriptableObject : ScriptableObject
{
    public abstract string Title { get; }
    public abstract string Description { get; }

    [field: NonSerialized] public bool Completed { get; private set; }
    public event Action OnCompleted;

    protected void SetCompleted()
    {
        Completed = true;
        OnCompleted?.Invoke();
    }

    public abstract void Initialize();
}