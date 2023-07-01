using SwiftLocator.Services.ServiceLocatorServices;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaySliderGameCommand : ICommand
{
    [SerializeReference] private List<INode> _winNodes;
    [SerializeReference] private List<INode> _loseNodes;

    public PlaySliderGameCommand(List<INode> winNodes, List<INode> loseNodes)
    {
        _winNodes = winNodes;
        _loseNodes = loseNodes;
    }
    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<SliderGame>().Begin(_winNodes, _loseNodes);
    }
}