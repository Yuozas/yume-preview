using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using static UnityEngine.SceneManagement.SceneManager;

public class Transitioner
{
    readonly private List<TransitionDestination> _destinations;
    readonly private InSceneCharacter _resolver;
    readonly private TransitionerAnimation _animation;

    private TransitionDestinationScriptableObject _to;

    public Transitioner()
    {
        _destinations = new();
        _resolver = ServiceLocator.GetSingleton<InSceneCharacter>();
        _animation = ServiceLocator.GetSingleton<TransitionerAnimation>();
    }

    public void Transition(TransitionDestinationScriptableObject to)
    {
        _to = to;
        _animation.ToDefault(Continue);
    }

    private void Continue()
    {
        var scene = GetActiveScene();
        if (scene.name == _to.SceneName)
        {
            TransitionToDestination(_to);
            return;
        }

        Load(_to);
    }

    public void Add(TransitionDestination destination)
    {
        _destinations.Add(destination);
    }

    public void Remove(TransitionDestination destination)
    {
        _destinations.Remove(destination);
    }

    private TransitionDestination Get(TransitionDestinationScriptableObject to)
    {
        return _destinations.First(destination => destination.This == to);
    }

    private async void Load(TransitionDestinationScriptableObject to)
    {
        await LoadSceneAsync(to.SceneName);
        TransitionToDestination(to);
    }

    private void TransitionToDestination(TransitionDestinationScriptableObject to)
    {
        var transitionable = (ITransitionable)_resolver.Get();
        Get(to).Transition(transitionable);

        _animation.ToClear();
    }
}
