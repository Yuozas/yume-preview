using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class SetChoicesCommand : ICommand
{
    private readonly Choice[] Choices;

    public SetChoicesCommand(params Choice[] choices)
    {
        Choices = choices;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Decisions>().Choices.Update(Choices);
    }
}