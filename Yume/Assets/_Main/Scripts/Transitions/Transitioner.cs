using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using static UnityEngine.SceneManagement.SceneManager;

public class Transitioner
{
    readonly private List<TransitionDestination> _destinations;
    readonly private CharacterResolver _resolver;
    readonly private TransitionerAnimation _animation;

    private Scriptable_TransitionDestination _to;

    public Transitioner()
    {
        _destinations = new();
        _resolver = ServiceLocator.GetSingleton<CharacterResolver>();
        _animation = ServiceLocator.GetSingleton<TransitionerAnimation>();
    }

    public void Transition(Scriptable_TransitionDestination to)
    {
        _to = to;
        _animation.ToDefault(Continue);
    }

    private void Continue()
    {
        var scene = GetActiveScene();
        if (scene.name == _to.Scene)
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

    private TransitionDestination Get(Scriptable_TransitionDestination to)
    {
        return _destinations.First(destination => destination.This == to);
    }

    private async void Load(Scriptable_TransitionDestination to)
    {
        await LoadSceneAsync(to.Scene);
        TransitionToDestination(to);
    }

    private void TransitionToDestination(Scriptable_TransitionDestination to)
    {
        var transitionable = (ITransitionable)_resolver.Resolve();
        Get(to).Transition(transitionable);

        _animation.ToClear();
    }
}
