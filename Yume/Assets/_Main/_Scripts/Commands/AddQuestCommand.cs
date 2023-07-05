using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class AddQuestCommand : ICommand
{
    [SerializeField] public QuestScriptableObject QuestScriptableObject;

    public AddQuestCommand(QuestScriptableObject questScriptableObject = null)
    {
        QuestScriptableObject = questScriptableObject;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Quests>().Add(QuestScriptableObject);
        onFinished?.Invoke();
    }
}